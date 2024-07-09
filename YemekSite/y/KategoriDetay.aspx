<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="KategoriDetay.aspx.cs" Inherits="YemekSite.Menuler.KategoriDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .styled-textbox {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            font-size: 16px;
        }

            .styled-textbox:focus {
                border-color: #66afe9;
                outline: none;
                box-shadow: 0 0 8px rgba(102, 175, 233, 0.6);
            }

        .styled-button {
            background-color: lawngreen;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

            .styled-button:hover {
                background-color: darkgreen;
            }

        table {
            width: 100%;
            margin: auto;
            border-collapse: collapse;
        }

        td {
            padding: 10px;
        }
    </style>

    bu kategoriye ait yemekler
    
    <table>
        <tr>
            <td>
                Kategori ismi: 
            </td>
            <td>
                <asp:TextBox ID="txtbox_kategoriad" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Kategori resim: 
            </td>
            <td >
                <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" />
                <asp:Button ID="btn_resimyukle" runat="server" Text="Resim Yükle" OnClick="btn_resimyukle_Click"/>
                <br />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:Image ID="Image1" runat="server" Width="100px" Height="100px"/> 
                
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnGuncelle" runat="server" Text="Kategori Güncelle" CssClass="styled-button" OnClick="btnGuncelle_Click" /><br />
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>

</asp:Content>
