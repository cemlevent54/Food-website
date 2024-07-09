<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="YorumlarDetay.aspx.cs" Inherits="YemekSite.Menuler.YorumlarDetay" %>

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
            font-family: Arial, Helvetica, sans-serif;
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
    </style>



    <table>
        <tr>
            <td>Ad Soyad:</td>
            <td>
                <asp:TextBox ID="txtbox_adsoyad" runat="server" CssClass="styled-textbox"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>Mail: </td>
            <td>
                <asp:TextBox ID="txtbox_mail" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Yorum Tarihi: </td>
            <td>
                <asp:TextBox ID="txtbox_yorumtarih" runat="server" CssClass="styled-textbox" TextMode="DateTime" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Yorum: </td>
            <td>
                <asp:TextBox ID="txtbox_yorum" runat="server" CssClass="styled-textbox" TextMode="MultiLine" Height="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Yemek:</td>
            <td>
                
                <asp:DropDownList ID="dropdown_yemekler" runat="server" Font-Size="18px" OnSelectedIndexChanged="dropdown_yemekler_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Butonlar</td>
            <td>
                <asp:Button ID="btn_yorumguncelle" runat="server" Text="Güncelle" CssClass="styled-button" OnClick="btn_yorumguncelle_Click"/>
                <asp:Button ID="btn_yorumonayla" runat="server" Text="Onayla" CssClass="styled-button" OnClick="btn_yorumonayla_Click"/>
            </td>
        </tr>
    </table>
    <asp:Label ID="lbl_sonuc" runat="server" Text=""></asp:Label>
</asp:Content>
