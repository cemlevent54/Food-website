<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="MesajlarDetay.aspx.cs" Inherits="YemekSite.Menuler.MesajlarDetay" %>

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
            <td>Ad Soyad: </td>
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
            <td>Konu: </td>
            <td>
                <asp:TextBox ID="txtbox_konu" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Mesaj: </td>
            <td>
                <asp:TextBox ID="txtbox_mesaj" runat="server" CssClass="styled-textbox" TextMode="MultiLine" Font-Names="Arial" Height="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Butonlar </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
