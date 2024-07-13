<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="ProfilBilgileri.aspx.cs" Inherits="YemekSite.ProfilBilgileri" %>
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
        width: 140px;  /* Set width to ensure two buttons fit within 300px */
        margin: 5px;  /* Add margin for spacing between buttons */
        text-align: center;
        box-sizing: border-box;
    }

        .styled-button:hover {
            background-color: darkgreen;
        }
    </style>
    <table>
        <tr>
            <td>Id : </td>
            <td>
                <asp:TextBox ID="txtbox_id" runat="server" Enabled="false" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>İsim : </td>
            <td>
                <asp:TextBox ID="txtbox_ad" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Soyisim : </td>
            <td>
                <asp:TextBox ID="txtbox_soyad" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>E-mail : </td>
            <td>
                <asp:TextBox ID="txtbox_email" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Kullanıcı Adınız : </td>
            <td>
                <asp:TextBox ID="txtbox_username" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Şifreniz :  </td>
            <td>
                <asp:TextBox ID="txtbox_sifre" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        
        
        <tr>
            <td>Telefon Numarası: </td>
            <td>
                <asp:TextBox ID="txtbox_telefon" runat="server" CssClass="styled-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Fotoğrafınız:  </td>
            <td>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:Image ID="img_userphoto" runat="server" Height="150px" Width="150px"/>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btn_resimguncelle" runat="server" Text="Resim Yükle" OnClick="btn_resimguncelle_Click" CssClass="styled-button" Width="100px"/>
                <%--<asp:TextBox ID="foto" runat="server"></asp:TextBox>--%>
                <asp:Label ID="lblresim" runat="server" Text=""></asp:Label>
            
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="display:flex;">
                <asp:Button ID="btn_guncelle" runat="server" Text="Güncelle" CssClass="styled-button" OnClick="btn_guncelle_Click" />
                <br />
                <asp:Label ID="lblsonuc" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="btn_kullaniciSil" runat="server" Text="Hesabımı Sil" OnClick="btn_kullaniciSil_Click" CssClass="styled-button" />
            </td>
        </tr>

    </table>
    </asp:Content>
