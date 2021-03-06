<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h1>
   <span class="hdr">SetHeight</span>
</h1>
<h4 class="header-gray" id="SetHeight">SetHeight(sHRule, nValue<sub>opt</sub>)</h4>
                
<p class="dscr">Set the height of the current table row within the current table.</p>

            <h2>Parameters:</h2>
            <table class="table">
                    <thead>
                        <tr class="tablerow">
                            <td>Name</td>
                            <td>Type</td>
                            <td>Attributes</td>
                            <td>Description</td>
                        </tr>
                    </thead>
                    <tbody>
                    <tr class="tablerow">
                        <td><em>sHRule</em></td>
                        <td>                
<em>"auto"</em>
|
<em>"atLeast"</em>
                        </td>
                        <td></td>
                        <td>The rule to either apply or ignore the height value 
to the current table row. Use the <em>"atLeast"</em> value to enable the <em>SetHeight</em> method use.</td>
                    </tr>
                        <tr class="tablerow">
                        <td><em>nValue</em></td>
                        <td>
                        <a href="<%= Url.Action("global") %>#twips">twips</a>
                        </td>
                        <td>&lt;optional><br></td>
                        <td>The height for the current table row measured in twentieths of a 
point (1/1440 of an inch). This value will be ignored if <em>sHRule="auto"</em>.</td>
                    </tr>
                </tbody>
                </table>

<h2>Example</h2>
<div class="button copy-code">Copy code</div>
<pre>builder.CreateFile("docx");
var oDocument = Api.GetDocument();
var oParagraph, oTable, oTableStyle, oCell, oTableRowPr;
oParagraph = oDocument.GetElement(0);
oParagraph.AddText("We create a 3x3 table and set the height of half an inch to all the rows:");
oTableStyle = oDocument.CreateStyle("CustomTableStyle", "table");
oTableStyle.SetBasedOn(oDocument.GetStyle("Bordered - Accent 5"));
oTable = Api.CreateTable(3, 3);
oTable.SetWidth("percent", 100);
oTable.SetTableLook(true, true, true, true, false, false);
oTableRowPr = oTableStyle.GetTableRowPr();
oTableRowPr.SetHeight("atLeast", 720);
oTable.SetStyle(oTableStyle);
oDocument.Push(oTable);
builder.SaveFile("docx", "SetHeight.docx");
builder.CloseFile();</pre>

<h2>Resulting document</h2>
<iframe class="docbuilder_resulting_docs" src="https://help.onlyoffice.com/products/files/doceditor.aspx?fileid=4896884&doc=dVJtUm9lbktscUJYZ2ZIZXIvMXhSeHdxclVDd0RwL0tQdi9vT1J6cFpBMD0_IjQ4OTY4ODQi0&action=embedded" frameborder="0" scrolling="no" allowtransparency></iframe>
