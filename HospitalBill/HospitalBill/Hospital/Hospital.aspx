<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hospital.aspx.cs" Inherits="HospitalBill.Hospital.Hospital" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Hospital Bill</title>
    <link href="../StyleSheets/Hospital.css" rel="stylesheet" type="text/css" />
    <script>

        function PrintData() {
            <%--            var doc = document.getElementById("<%= Panel1.ClientID %>");
            var movwindow = window.open("", "", "height=500", "width=600");
            movwindow.document.write("<html><head><title>Print Page</title>");
            movwindow.document.write('<link href="../StyleSheets/Hospital.css" rel="stylesheet" type="text/css" />');
            movwindow.document.write("</head><body>");
            movwindow.document.write(doc.innerHTML);
            movwindow.document.write("</body></html>");
            movwindow.print();--%>
            window.print();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:Panel ID="Panel1" runat="server">--%>
        <div class="containerr">

            <div class="heading">
                <div>
                    <h3 style="color: #0094ff">Hospital Bill</h3>
                </div>
            </div>

            <div class="subcontainer">
                <div>
                    <asp:Label ID="Label1" runat="server" Text="Bill Number"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                </div>

                <div>
                    <asp:Label ID="Label2" runat="server" Text="Bill Date"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                </div>
            </div>

            <div class="subcontainer">
                <div style="padding-left: 103px">
                    <asp:Label ID="Label3" CssClass="patient" runat="server" Text="Patient Name"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="validationcheck" runat="server" ControlToValidate="TextBox3" ErrorMessage="Name should be characters" ForeColor="Red" ValidationExpression="^[a-z A-Z /s]+$"></asp:RegularExpressionValidator>
                </div>
                <div>
                    <asp:Label ID="Label4" runat="server" Text="Gender"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" CssClass="genderdrop" runat="server">
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem> Female</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="padding-right: 41px;">
                    <asp:Label ID="Label6" runat="server" Text="Date of Birth"></asp:Label>
                    <input id="Date1" class="date-time" type="date" runat="server" />
                </div>
            </div>

            <div class="subcontainer">
                <div class="addres" style="padding-left: 135px; margin-top: -5px;">
                    <div style="padding-top: 10px">
                        <asp:Label ID="Label8" CssClass="addresslabel" runat="server" Text="Address"></asp:Label>
                    </div>
                    <div>
                        <textarea id="TextArea1" class="addborder" runat="server" cols="20" rows="2"></textarea>
                    </div>
                </div>

                <div style="padding-left: 20px;">
                    <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox7" CssClass="validationcheck" ErrorMessage="Enter Valid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
                <div style="padding-right: 21px;">
                    <asp:Label ID="Label10" runat="server" Text="Mobile"></asp:Label>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox8" CssClass="validationcheck" ErrorMessage="Enter Valid Phone Number" ForeColor="Red" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="subcontainer">
                <div style="padding-left: 143px">
                    <asp:Label ID="Label5" runat="server" Text="Investigtion"></asp:Label>
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="genderdrop" AutoPostBack="True">
                        <asp:ListItem>-- Select --</asp:ListItem>
                        <asp:ListItem>Fever</asp:ListItem>
                        <asp:ListItem>Malaria</asp:ListItem>
                        <asp:ListItem>Thyphodi</asp:ListItem>
                        <asp:ListItem>Skin Infection</asp:ListItem>
                        <asp:ListItem>Diearea</asp:ListItem>
                        <asp:ListItem>Heart problem</asp:ListItem>
                        <asp:ListItem>Stomach problem</asp:ListItem>
                        <asp:ListItem>Headache</asp:ListItem>
                        <asp:ListItem>Migration</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="padding-left: 84px">
                    <asp:Label ID="Label7" runat="server" Text="Fee"></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" Enabled="false" ReadOnly="true" EnableViewState="False"></asp:TextBox>
                </div>
                <div style="padding-right: 231px">
                    <asp:Button ID="Button1" CssClass="bttn" runat="server" Text="Add To Grid" OnClick="Button1_Click" />
                </div>
            </div>


            <div class="grid">
                <div dir="ltr">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="Sno" DataSourceID="SqlDataSource1" Height="36px" Width="569px">
                        <Columns>
                            <asp:BoundField DataField="Problem" HeaderText="Problem" SortExpression="Problem" />
                            <asp:BoundField DataField="Sno" HeaderText="Sno" InsertVisible="False" ReadOnly="True" SortExpression="Sno" />
                            <asp:BoundField DataField="Fees" HeaderText="Fees" SortExpression="Fees" />
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button">
                                <ControlStyle CssClass="Stylebttns" />
                            </asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connectionStringData %>" DeleteCommandType="StoredProcedure" DeleteCommand="DeleteProblem" SelectCommand="GetAllDiseaseById" SelectCommandType="StoredProcedure" UpdateCommand="UpdateProblem" UpdateCommandType="StoredProcedure">
                        <DeleteParameters>
                            <asp:Parameter Name="Sno" Type="Int32" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="PatientId" SessionField="id" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Problem" Type="String" />
                            <asp:Parameter Name="Fees" Type="String" />
                            <asp:Parameter Name="Sno" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

            <div class="bttns">
                <div>
                    <asp:Button ID="Button2" runat="server" CssClass="buttns addbtn" Text="Add" OnClick="Button2_Click" />
                </div>
                <div>
                    <asp:Button ID="Button3" runat="server" CssClass="buttns editbtn" Text="Edit" OnClick="Button3_Click" />
                </div>
                <div>
                    <asp:Button ID="Button4" runat="server" CssClass="buttns savebtn" Text="Save" OnClick="Button4_Click" />
                </div>
                <div>
                    <asp:Button ID="Button5" runat="server" CssClass="buttns clearbtn" Text="Clear" OnClick="Button5_Click" CausesValidation="False" />
                </div>
                <div>
                    <asp:Button ID="Button6" runat="server" CssClass="buttns exportbtn" Text="Export" OnClick="Button6_Click" CausesValidation="False" />
                </div>
                <div>
                    <asp:Button ID="Button7" runat="server" CssClass="buttns printbtn" Text="Print" OnClientClick="return PrintData()" CausesValidation="False" />
                </div>
            </div>
        </div>
        <%--</asp:Panel>--%>
        <%--   <div>
            <div>
                <asp:Button ID="Button8" runat="server" Text="Button" OnClick="Button8_Click" />
            </div>
        </div>--%>
        <div>
            <div>
                <asp:GridView ID="gvProducts" runat="server"></asp:GridView>
                <asp:Button ID="Button9" runat="server" Text="Button" OnClick="Button9_Click" />
            </div>
        </div>
    </form>

</body>

</html>
