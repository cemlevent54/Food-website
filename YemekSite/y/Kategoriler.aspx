<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Kategoriler.aspx.cs" Inherits="YemekSite.Menuler.Kategoriler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        .button-space {
            margin-right: 10px;
        }

            .button-space:last-child {
                margin-right: 0;
            }

        .text-right {
            text-align: right;
        }

        .file-upload-container {
            margin-bottom: 15px;
            display: flex;
            align-items: center;
            width: 100%;
        }

        .file-upload-buttons {
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .file-upload {
            display: none;
        }

        .file-upload-button {
            padding: 10px;
            border: none;
            border-radius: 4px;
            background-color: #007bff;
            color: white;
            font-size: 16px;
            cursor: pointer;
            text-align: center;
            box-sizing: border-box;
            margin-bottom: 10px;
            margin-right: 100px;
        }

            .file-upload-button:hover {
                background-color: #0056b3;
            }

        .image-box {
            margin-left: 20px;
        }

        .file-upload-select {
            cursor: pointer;
        }

        .signup-image {
            width: 30px;
            height: 30px;
            border: 1px solid #ccc;
            margin-top: 10px;
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
            background-color: lightgreen;
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


    <asp:Panel ID="pnl_kategoriListesi_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_kategorigoster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_kategorigoster_Click" />
                    <asp:Button ID="btn_kategorigizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_kategorigizle_Click" />
                </td>

                <td>
                    <asp:Label ID="lblkategori" runat="server" Text="Kategori Listesi" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_kategoriListesi" runat="server">
        <asp:DataList ID="dtlist_Kategori" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("kategori_ad") %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("kategori_id") %>' OnClientClick='return confirm("Silmeyi onaylıyor musunuz?");'>
                                <asp:Image ID="img_delete" runat="server" ImageUrl="~/images/deleteicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CommandArgument='<%# Eval("kategori_id") %>'>
                                <asp:Image ID="img_refresh" runat="server" ImageUrl="~/images/refreshicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>

    <asp:Panel ID="pnl_kategoriEkleme_baslik" runat="server" BackColor="GrayText">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_goster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_goster_Click" />
                    <asp:Button ID="btn_gizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gizle_Click" />
                </td>

                <td>
                    <asp:Label ID="Label2" runat="server" Text="Kategori Ekleme" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_kategoriEkleme" runat="server">
        <table>
            <tr>
                <td>Kategori Adı:</td>
                <td>
                    <asp:TextBox ID="txt_kategoriAd" runat="server" CssClass="styled-textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Kategori resim: 
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" />
                    <asp:Button ID="btn_resimyukle" runat="server" Text="Resim Yükle" OnClick="btn_resimyukle_Click" />
                    <br />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:Image ID="Image1" runat="server" Width="100px" Height="100px" />

                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btn_KategoriEkle" runat="server" Text="Ekle" OnClick="btn_KategoriEkle_Click" CssClass="styled-button" />
                    <asp:Label ID="lblsonuc" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
