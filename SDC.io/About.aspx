<%@ Page Title="" Language="C#" MasterPageFile="~/SDC.io.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication1.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMaster" runat="server">
    <div class="container">
        <h2>About</h2>
        <p>
            In a writer’s personality, writing style has an important role. The first step in this paper describes the way of applying a
        <abbr title="Sequence to sequence">seq2seq</abbr>
            translation model, with global attention mechanism, to convert a document to a different writing style. The study also shows teaching mode for a network with backpropagation algorithm. The bidirectional Recurrent Neural Network with a
            <abbr title="Sequence to sequence">Seq2Seq</abbr>
            model, improved other models' methodologies for style conversion. Moreover, with the human evaluated metrics, we could observe that our bidirectional
            <abbr title="Sequence to sequence">Seq2Seq</abbr>
            model performed better than our simple attentive
            <abbr title="Sequence to sequence">Seq2Seq</abbr>
            model in preserving original meaning and imitating target style.
        </p>
        <p>The second step in this paper implies the usage of a methodology for patterning writing style evolution using dynamic similarity. Using division of a text into separated portions of the same size and utilize the Mean Dependence Measure, aiming to model the writing process by a connection between the current text portion and its predecessors. A two-step clustering procedure is employed to reveal the evolution of a style. </p>
        <p>The goal of this study is creating a system which competent to determine whether two documents are written with the same writing style, or not. The way to check the result is to use style conversion and prove the output <strong>does not change</strong>.</p>
    </div>
</asp:Content>
