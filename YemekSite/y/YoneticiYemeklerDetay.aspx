<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="YoneticiYemeklerDetay.aspx.cs" Inherits="YemekSite.Menuler.YoneticiYemeklerDetay" %>

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
            padding: 10px 0; /* Adjust padding to fit within 300px */
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px; /* Adjust font size to fit within 300px */
            width: 130px; /* Set width to ensure two buttons fit within 300px */
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

    <table>
        <tr>
            <td>Yemek Adı:</td>
            <td>
                <asp:TextBox ID="txtbox_yemekadi" runat="server" Width="250px" Font-Size="15px" CssClass="styled-textbox"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>Yemek Malzemeleri: </td>
            <td>
                <asp:TextBox ID="txtbox_yemekmalzemeleri" runat="server" TextMode="MultiLine" Width="290px" Height="195px" Font-Size="15px" CssClass="styled-textbox" Font-Names="Arial"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>Yemek Tarifi: </td>
            <td>
                <asp:TextBox ID="txtbox_yemektarifi" runat="server" TextMode="MultiLine" Width="290px" Height="195px" Font-Size="15px" CssClass="styled-textbox" Font-Names="Arial"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Yemek yayınlanma tarih: </td>
            <td>
                <asp:TextBox ID="txtbox_yemektarihi" runat="server" Width="290px" Font-Size="15px" CssClass="styled-textbox" Enabled="False" Font-Bold="true"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>Yemek puanı: </td>
            <td>
                <asp:TextBox ID="txtbox_yemekpuan" runat="server" Width="290px" Font-Size="15px" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <%--<td>Yemek Kategori id: </td>
            <td >
                

            </td>--%>
            <asp:TextBox ID="txtbox_kategoriId" runat="server" Width="290px" Font-Size="15px" CssClass="styled-textbox" Visible="false"></asp:TextBox>
        </tr>

        <tr>
            <td>Yemek Kategorisi: </td>
            <td>
                <asp:DropDownList ID="dropdown_kategoriId" runat="server" Width="200px" Height="25px" Font-Size="20px" OnSelectedIndexChanged="dropdown_kategoriId_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Yemek Resmi: </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btn_resimekle" runat="server" Text="Resim Yükle" OnClick="btn_resimekle_Click" />
                <asp:Image ID="img_yemekresim_yukleme" runat="server" Width="100px" Height="100px" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btn_guncelle" runat="server" Text="Güncelle" CssClass="styled-button" OnClick="btn_guncelle_Click" />
                <asp:Button ID="btn_onayla" runat="server" Text="Onayla" CssClass="styled-button" OnClick="btn_onayla_Click" />
                <asp:Button ID="btn_ekle" runat="server" Text="Ekle" CssClass="styled-button" OnClick="btn_ekle_Click" />
            </td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Label ID="lbl_mesaj" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>

</asp:Content>
