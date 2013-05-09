<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="dist_sys_ass_2._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Distributed Systems Assignment 2</title>
    <style type="text/css">
        body
        {
            background-color: Lime;
            font-family: Comic Sans MS;
        }
        
        table
        {
            margin-left: auto;
            margin-right: auto;
            background-color: Yellow;
            width: 85%;
        }
        
        h2
        {
            text-align: center;
        }
        
        .center_align
        {
            text-align: center;
        }
    </style>

    <script type="text/javascript">
        function validate_fname(src, args) {
            var val = document.getElementById("tb_fname").value;
            if (val.length < 5 || val.length > 32) {
                args.isValid = false;
            }
            if (new RegExp("\d+", g).test(val) == true) {
                args.isValid = false;
            }
            args.isValid = true;
        }
    </script>
</head>
<body>
    <table border="1">
        <tr>
            <td colspan="2">
                <h2>Distributed Systems Assignment 2</h2>
            </td>
        </tr>

        <tr>
            <td>
                <h3>Instructions:</h3>

                <p>Add: fill in all fields (ID is not necessary) and click on Add Member.</p>

                <p>Search: Fill in all fields relevant to your search (cannot search by database ID) and click on
                Search. To bring up a members data in the editing window click on the select button in the grid.</p>

                <p>Delete: First, search for the member you wish to delete, select them by hitting the select button to
                bring their information to the editing window, then click on delete. The grid will then display no results,
                this means that it has attempted to search for the deleted member and failed, confirming the deletion.</p>

                <p>Modify: First, search for the member you wish to modify, select them by hitting the select button to
                bring their information to the editing window. Modify the relevant data and then click on Modify. The
                grid will then display only the modified member with their new updated details, confirming the modification.</p>

                <form id="stuff_form" class="center_align" runat="server">
                    <asp:Label ID="lbl_id" runat="server">ID: </asp:Label>
                    <br />
                    <asp:TextBox ID="tb_ID" runat="server" Enabled="false"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lbl_fname" runat="server">First Name: </asp:Label>
                    <br />
                    <asp:TextBox ID="tb_fname" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lbl_lname" runat="server">Last Name: </asp:Label>
                    <br />
                    <asp:TextBox ID="tb_lname" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lbl_memtype" runat="server">Member Type: </asp:Label>
                    <br />
                    <asp:TextBox ID="tb_memtype" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lbl_verification" runat="server">Verification Code: </asp:Label>
                    <br />
                    <asp:TextBox ID="tb_vcode" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btn_clear" runat="server" Text="Clear" OnClick="Clear" />
                    <br />
                    <br />
                    <asp:Button ID="btn_search" runat="server" Text="Search Member" OnClick="Search" />
                    &nbsp;
                    <asp:Button ID="btn_add" runat="server" Text="Add Member" OnClick="Add" />
                    &nbsp;
                    <asp:Button ID="btn_delete" runat="server" Text="Delete Member" OnClick="Delete" />
                    &nbsp;
                    <asp:Button ID="btn_modify" runat="server" Text="Modify Member" OnClick="Modify" />
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        AllowPaging="True" SelectedRowStyle-BackColor="Orange" OnSelectedIndexChanged="Update">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="MembershipId" HeaderText="ID" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                            <asp:BoundField DataField="MembershipClass" HeaderText="Membership Class" />
                            <asp:BoundField DataField="VerificationCode" HeaderText="Verification Code" />
                        </Columns>
                    </asp:GridView>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
