<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="YemekDetay.aspx.cs" Inherits="YemekSite.YemekDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        table {
            table-layout: auto;
        }

        .baslik {
            font-size: 30px;
            font-weight: bold;
            color: #000000;
        }

        .descriptions {
            font-size: 20px;
            font-weight: bold;
        }

        .definitions {
            font-size: 18px;
        }

        #img_yemekresim {
            border: 1px solid black;
            text-align: center;
        }

        .txtBox {
            height: 20px;
            width: 200px;
            font-size: 17px;
        }

        #btn_yorumyap {
            background-color: black;
            color: white;
        }

            #btn_yorumyap:hover {
                background-color: white;
                color: black;
            }

        #txtbox_yorum {
            height: 170px;
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

    <div style="display: flex; flex-direction: column; justify-content: center; align-items: center; text-align: center;">
        <%--Yemek Detay Tablosu Başlangıç--%>
        <asp:DataList ID="dtlist_yemekdetay" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <asp:Label ID="LabelYemekAdi" runat="server" Text='<%# Eval("yemek_ad") %>' BackColor="#FFFF66" Font-Size="30px" Font-Bold="True" ForeColor="#FF6666" Style="display: block; text-align: center; margin-bottom: 10px;">merhaba</asp:Label>
                <asp:Image ID="img_yemekresim" runat="server" Width="200px" Height="200px" ImageUrl='<%# Eval("yemek_resim_base64") %>' Style="display: block; margin: auto; margin-bottom: 10px;" />

                <table border="0" cellpadding="5" cellspacing="0" style="width: 100%; border-collapse: initial; margin-bottom: 10px;">
                    <tr>
                        <td style="font-weight: bold; padding-right: 10px;">Yemek Malzemeleri:</td>
                        <td>
                            <asp:Label ID="lbl_yemek_malzeme" runat="server" Text='<%# Eval("yemek_malzeme") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; padding-right: 10px;">Yemek Tarifi:</td>
                        <td>
                            <asp:Label ID="lbl_yemek_tarif" runat="server" Text='<%# Eval("yemek_tarif") %>'></asp:Label>
                        </td>
                    </tr>
                </table>

                <table border="0" cellpadding="5" cellspacing="0" style="width: 100%; border-collapse: initial;">
                    <tr>
                        <td style="font-weight: bold; padding-right: 10px;">Tarih:</td>
                        <td>
                            <asp:Label ID="lbl_yemek_tarih" runat="server" Text='<%# Eval("yemek_tarih") %>' Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; padding-right: 10px;">Yemek Puan:</td>
                        <td>
                            <asp:Label ID="lbl_yemek_puan" runat="server" Text='<%# Eval("yemek_puan") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <%--Yemek Detay Tablosu Bitiş--%>

        <asp:Label ID="lbl_baslik" runat="server" Text="Yapılan Yorumlar" Width="200px" Height="25px" BackColor="#FFFFCC" Font-Size="25px"></asp:Label>
        <div style="height: 10px;"></div>
        <%-- Boşluk eklemek için --%>
        <%--yapılan yorumları goruntuleme baslangic--%>

        <asp:DataList ID="dtlist_yorumlar" runat="server" CellPadding="4" ForeColor="#333333" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px" Width="100%">
            <AlternatingItemStyle BackColor="White" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid"></AlternatingItemStyle>

            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>

            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>

            <ItemStyle BackColor="#E3EAEB"></ItemStyle>
            <ItemTemplate>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblyorum_isimsoyisim" runat="server" Text='<%# Eval("yorum_adsoyad") %>' Font-Bold="True" Font-Size="25px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblyorum_icerik" runat="server" Text='<%# Eval("yorum_icerik") %>' Font-Size="15px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblyorum_tarih" runat="server" Text='<%# Eval("yorum_tarih") %>' Font-Size="10px" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedItemStyle>
        </asp:DataList>
        <%--yapılan yorumları goruntuleme bitis--%>

        <br />

        <%--Yorum ekleme bölümü başlangıç--%>
        <div>

            <asp:Label ID="Label1" runat="server" Text="Eğer Beğendiyseniz Aşağıya Yorumlarınızı Paylaşabilirsiniz" BackColor="#FFFFCC"></asp:Label>
            <br />

            <table>
                <tr>
                    <td class="descriptions">Ad Soyad:

                    </td>
                    <td>
                        <asp:TextBox ID="txtbox_isimsoyisim" runat="server" Font-Size="17px" Width="300px" Height="20px" Font-Names="Arial" CssClass="styled-textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="descriptions">Mail:

                    </td>
                    <td>
                        <asp:TextBox ID="txtbox_mail" runat="server" Font-Size="17px" Width="300px" Height="20px" Font-Names="Arial" CssClass="styled-textbox"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="descriptions">Yorumunuz:

                    </td>
                    <td>
                        <asp:TextBox ID="txtbox_yorum" runat="server" TextMode="MultiLine" Width="300px" Height="170px" Font-Size="17px" Font-Names="Arial" CssClass="styled-textbox"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="descriptions"></td>
                    <td>
                        <asp:Button ID="btn_yorumyap" runat="server" Text="Yorum Yap" Font-Names="Arial" OnClick="btn_yorumyap_Click" CssClass="styled-button"/>

                    </td>
                </tr>
            </table>
            <asp:Label ID="lblsonuc" runat="server" Text=""></asp:Label>

        </div>
        <%--Yorum ekleme bölümü bitiş--%>
    </div>

</asp:Content>
