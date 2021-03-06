<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h1>
   <span class="hdr">SetStyleColBandSize</span>
</h1>
<h4 class="header-gray" id="SetStyleColBandSize">SetStyleColBandSize(nCount)</h4>
                
<p class="dscr">Specify the number of columns which will comprise each table column band for this table style.</p>

            <h2>Parameters:</h2>
            <table class="table">
                    <thead>
                        <tr class="tablerow">
                            <td>Name</td>
                            <td>Type</td>
                            <td>Description</td>
                        </tr>
                    </thead>
                    <tbody>
                    <tr class="tablerow">
                        <td><em>nCount</em></td>
                        <td>
                        <em>number</em>
                        </td>
                        <td>The number of columns measured in positive integers.</td>
                    </tr>
                </tbody>
                </table>

<h2>Example</h2>
<div class="button copy-code">Copy code</div>
<pre>builder.CreateFile("docx");
var oDocument = Api.GetDocument();
var oParagraph, oTable, oTableStyle, oTablePr;
oDocument.RemoveAllElements();
oTableStyle = oDocument.CreateStyle("CustomTableStyle", "table");
oTable = Api.CreateTable(4, 2);
oTable.SetWidth("percent", 100);
oTable.SetStyle(oTableStyle);
oTablePr = oTableStyle.GetTablePr();
oTable.SetTableLook(true, true, true, true, true, true);
oTablePr.SetStyleColBandSize(2);
oTableStyle.GetConditionalTableStyle("bandedColumn").GetTextPr().SetBold(true);
oTable.GetRow(0).GetCell(0).GetContent().GetElement(0).AddText("Bold");
oTable.GetRow(0).GetCell(1).GetContent().GetElement(0).AddText("Bold");
oTable.GetRow(0).GetCell(2).GetContent().GetElement(0).AddText("Normal");
oTable.GetRow(0).GetCell(3).GetContent().GetElement(0).AddText("Normal");
oTable.GetRow(1).GetCell(0).GetContent().GetElement(0).AddText("Bold");
oTable.GetRow(1).GetCell(1).GetContent().GetElement(0).AddText("Bold");
oTable.GetRow(1).GetCell(2).GetContent().GetElement(0).AddText("Normal");
oTable.GetRow(1).GetCell(3).GetContent().GetElement(0).AddText("Normal");
oDocument.Push(oTable);
builder.SaveFile("docx", "SetStyleColBandSize.docx");
builder.CloseFile();</pre>

<h2>Resulting document</h2>
<iframe class="docbuilder_resulting_docs" src="https://help.onlyoffice.com/products/files/doceditor.aspx?fileid=4912862&doc=YmNtQTF1TEFQR2tnQThIYWg2THBzditDZ0Q2S29VRHM5S0VLVXZRbDI1cz0_IjQ5MTI4NjIi0&action=embedded" frameborder="0" scrolling="no" allowtransparency></iframe>
