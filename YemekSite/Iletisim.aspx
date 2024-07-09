<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="Iletisim.aspx.cs" Inherits="YemekSite.Iletisim1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        td {
            font-size: 20px;
        }

        .textboxes_large {
            border: 1px solid black;
            border-radius: 5px;
            height: 100px;
            width: 300px;
            font-family: Arial;
            font-size: 15px;
        }

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
            padding: 10px 0; /* Adjust padding to fit within 300px */
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px; /* Adjust font size to fit within 300px */
            width: 140px; /* Set width to ensure two buttons fit within 300px */
            margin: 5px; /* Add margin for spacing between buttons */
            text-align: center;
            box-sizing: border-box;
        }

            .styled-button:hover {
                background-color: darkgreen;
            }

        table {
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>

    <div style="display: flex;flex-direction:column; justify-content: center; align-items: center; text-align: center;">
        <h2 style="background-color:antiquewhite;"> İletişim </h2>
        <table>
            <tr>
                <td>Ad Soyad: </td>
                <td>
                    <asp:TextBox ID="txtBox_adSoyad" runat="server" CssClass="styled-textbox" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Mail: </td>
                <td>
                    <asp:TextBox ID="txtBox_mail" runat="server" CssClass="styled-textbox" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Konu: </td>
                <td>
                    <asp:TextBox ID="txtBox_konu" runat="server" CssClass="styled-textbox" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Mesaj: </td>
                <td>
                    <asp:TextBox class="textboxes_large" ID="txtBox_mesaj" runat="server" TextMode="MultiLine" CssClass="styled-textbox" Height="300px" Width="300px" Font-Names="Arial"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btn_gonder" runat="server" Text="Gönder" OnClick="btn_gonder_Click" CssClass="styled-button" />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>


</asp:Content>
