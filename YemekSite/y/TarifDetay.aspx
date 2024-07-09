<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="TarifDetay.aspx.cs" Inherits="YemekSite.Menuler.TarifDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .image-box {
            margin-top: 15px;
            width: 150px;
            height: 150px;
            border: 2px solid #ddd;
            border-radius: 4px;
            overflow: hidden;
        }

        .signup-image {
            width: 100%;
            height: 100%;
            object-fit: cover; /* Resimlerin kare içine sığmasını sağlar */
        }

        #txtBox_confirmPassword {
            margin-left: 200px;
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
    </style>


    <table>
        <tr>
            <td>Tarif Adı: </td>
            <td>
                <asp:TextBox ID="txtbox_tarifad" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Tarif Malzemeleri: </td>
            <td>
                <asp:TextBox ID="txtbox_tarifmalzemeleri" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Tarif Yapılışı: </td>
            <td>
                <asp:TextBox ID="txtbox_tarifyapilisi" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Tarif Sahibi: </td>
            <td>
                <asp:TextBox ID="txtbox_tarifsahibi" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Tarif Sahibinin Maili: </td>
            <td>
                <asp:TextBox ID="txtbox_tarifsahibimail" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Tarif Resim: </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btn_resimekle" runat="server" Text="Resim Yükle" OnClick="btn_resimekle_Click" />
                <asp:Image ID="img_yemekresim_yukleme" runat="server" Width="100px" Height="100px" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
            </td>
        </tr>

        <tr>
            <td></td>
            <td style="display: flex">
                <asp:Button ID="btn_tarifGuncelle" runat="server" Text="Tarif Güncelle" CssClass="styled-button" OnClick="btn_tarifGuncelle_Click" />
                <asp:Button ID="btn_tarifOnayla" runat="server" Text="Tarifi Onayla" CssClass="styled-button" OnClick="btn_tarifOnayla_Click" />
                <asp:Button ID="btn_tarifiEkle" runat="server" Text="Tarifi Yemeklere Ekle" CssClass="styled-button" OnClick="btn_tarifiEkle_Click" />
            </td>


        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Label ID="lbl_mesaj" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
        

        <asp:HiddenField ID="HiddenField1" runat="server" />
    </table>

</asp:Content>
