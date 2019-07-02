# SDC<span>.io</span>
Style Detection by Conversion
This project implements a WEB Site that visualizes the detection of numerous writing styles in a text. This WEB UI is implemented using ASP<span>.NET</span>, JQuery, AngularJS, Bootstrap, SQL Server and Python scripts that use the [Multiple Writing Style Detector](https://github.com/romanglo/multiple-writing-style-detector) package to clarify the amount of writing styles in the texts.

## Quickstart

#### Requirements

**Note!** 

The project was written in following versions:

1. Python's version 3.6.8.
1. .NET's version 4.7.2

---

The project contains two important requirements:

1. First, and a very significant part is the [Multiple Writing Style Detector](https://github.com/romanglo/multiple-writing-style-detector) package. This package is created mainly for the use of this project, and implements a solution of detecting numerous writing styles in a text.
1. Next is the [OpenNMT-py](https://github.com/OpenNMT/OpenNMT-py) package which is used to translate the source texts into another writing style.

Both of the package are installed using the following steps:

1. Download the packages to the local computer.
1. Run the following commands for both of the packages: 

    ```bash
    pip install -r requirements.txt
    ```

    ```bash
    python setup.py install
    ```



## Authors

* *[Yoni Shpund](https://github.com/YoniShpund)*
* *[Roman Glozman](https://github.com/romanglo)*

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for mode details.
