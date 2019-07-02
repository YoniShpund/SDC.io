from __future__ import unicode_literals

import argparse
import codecs
import json
import logging
import os
import sys

import mwsd

DEFAULT_VERBOSE = True
DEFAULT_SAVE_OUTPUT = True


def read_arguments(argv):
    ap = argparse.ArgumentParser(
        formatter_class=argparse.ArgumentDefaultsHelpFormatter)
    ap.add_argument(
        "-fi",
        "--first_input",
        type=str,
        required=True,
        help="First text path")
    ap.add_argument(
        "-si",
        "--second_input",
        type=str,
        required=True,
        help="Second text path")
    ap.add_argument(
        "-so",
        "--save_output",
        type=bool,
        default=DEFAULT_SAVE_OUTPUT,
        required=False,
        help="Save the results to execution folder")
    ap.add_argument(
        "-dir",
        "--output_dir",
        type=str,
        required=False,
        default=".",
        help="Output directory path")
    ap.add_argument(
        "-name",
        "--output_name",
        type=str,
        required=False,
        default="mwsd_result",
        help="Output files name")
    ap.add_argument(
        "-mp",
        "--model_path",
        type=str,
        default=None,
        required=False,
        help="The path to word2vec model")
    ap.add_argument(
        "-v",
        "--verbose",
        type=bool,
        default=DEFAULT_VERBOSE,
        required=False,
        help="Verbose logging")

    args, _ = ap.parse_known_args()
    argsDict = vars(args)

    return argsDict


def initialize_logging_config(logging_level):
    logging.basicConfig(
        format=
        u'%(asctime)s %(name)-7s [%(filename)10s:%(lineno)3s - %(funcName)20s()] %(levelname)-8s:: %(message)s.',
        level=logging_level)


def process(first_input, second_input, save_output, output_dir, model_path,
            file_name):
    mwsd.initialize()

    model_path = None if (not model_path
                          or not os.path.isfile(model_path)) else model_path

    if not os.path.isfile(first_input):
        raise IOError(
            "First input file does not exist: {}".format(first_input))
    if not os.path.isfile(second_input):
        raise IOError(
            "Second input file does not exist: {}".format(second_input))

    model = None if not model_path else mwsd.word2vec.load_model(
        model_path, keyed_vectors=True, binary=True)

    first_text, second_text = mwsd.utils.read_text_from_files(
        [first_input, second_input], encoding='utf-8')

    ZV, DZV, clustering_result = mwsd.execute(first_text, second_text, model)

    plot_saving_path = os.path.join(
        output_dir, f'{file_name}.png') if save_output else None

    mwsd.visualize_algorithm_result(
        ZV,
        DZV,
        clustering_result,
        show_plot=False,
        plot_saving_path=plot_saving_path)


def main(args):
    try:
        initialize_logging_config(
            logging.DEBUG if args['verbose'] else logging.INFO)
        process(args['first_input'], args['second_input'], args['save_output'],
                args['output_dir'], args['model_path'], args["output_name"])
    except KeyboardInterrupt:
        logging.info("Process aborted by the user!")
    except Exception:
        logging.exception(
            "Some error occurred during the running! Process aborted..")


if __name__ == "__main__":
    args = read_arguments(sys.argv[1:])
    main(args)
