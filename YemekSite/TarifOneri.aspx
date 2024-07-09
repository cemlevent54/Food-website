<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="TarifOneri.aspx.cs" Inherits="YemekSite.TarifOneri1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #resim-display {
            font-size: 15px;
            padding-right: 10px;
        }



        .custom_button {
            background-color: whitesmoke; /* Arka plan rengi */
            border: none; /* Kenarlık yok */
            color: black; /* Metin rengi */
            padding: 15px 32px; /* İç boşluk (padding) */
            text-align: center; /* Metin hizalaması */
            text-decoration: none; /* Metin alt çizgisi yok */
            display: inline-block; /* Satır içi blok seviyesi eleman */
            font-size: 16px; /* Yazı boyutu */
            margin: 4px 2px; /* Dış boşluk (margin) */
            cursor: pointer; /* İmleç tipi */
            border-radius: 12px; /* Kenar yuvarlama */
        }

            .custom_button:hover {
                background-color: black; /* Arka plan rengi */
                border: none; /* Kenarlık yok */
                color: whitesmoke; /* Metin rengi */
            }

        .image_upload {
            background-color: whitesmoke; /* Arka plan rengi */
            border: none; /* Kenarlık yok */
            color: black; /* Metin rengi */
            padding: 15px 15px; /* İç boşluk (padding) */
            text-align: center; /* Metin hizalaması */
            text-decoration: none; /* Metin alt çizgisi yok */
            display: inline-block; /* Satır içi blok seviyesi eleman */
            font-size: 16px; /* Yazı boyutu */
            margin: 4px 4px; /* Dış boşluk (margin) */
            cursor: pointer; /* İmleç tipi */
            border-radius: 12px; /* Kenar yuvarlama */
            margin-right: 50px;
        }

            .image_upload:hover {
                background-color: black; /* Arka plan rengi */
                border: none; /* Kenarlık yok */
                color: whitesmoke; /* Metin rengi */
            }

        .imgbox {
            margin-top: 30px;
        }



        .container {
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .upload-section {
            flex: 1;
        }

        /*xxsa*/

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
        <table>
            <tr style="text-align: center; font-size: 30px; background-color: antiquewhite">
                <td colspan="2" style="text-align: center; padding: 10px;">TARİF ÖNER</td>
            </tr>
            <tr style="text-align: center;">
                <td>Tarif Adı: </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBox_tarifAdi" CssClass="styled-textbox" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: center;">
                <td>Tarif Malzemeleri: </td>
                <td>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtBox_tarifMalzemeleri" CssClass="styled-textbox" Height="300px" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: center;">
                <td>Yapılışı: </td>
                <td>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtBox_tarifYapilis" CssClass="styled-textbox" Height="300px" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: center;">
                <td>Tarifi Öneren Kişi: </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBox_tarifOnerenKisi" CssClass="styled-textbox" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: center;">
                <td>Mail Adresi: </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBox_Mail" OnTextChanged="txtBox_Mail_TextChanged" AutoPostBack="True" CssClass="styled-textbox" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: center;">
                <td>Resim: </td>
                <td>
                    <div class="container">
                        <div class="upload-section">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" Height="30px" />
                            <asp:Button ID="Button1" runat="server" Text="Resim Yükle" OnClick="Button1_Click" CssClass="styled-button" Width="200px" BackColor="#99ff99" />
                        </div>
                        <div class="image-section">
                            <div class="imagebox">
                                <asp:Image ID="Image1" runat="server" ImageUrl="" Height="100px" Width="100px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div style="text-align: center;">
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:Button CssClass="styled-button" ID="btn_onay" runat="server" Text="Onayla" OnClick="btn_onay_Click" />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
    </div>






    <script type="text/javascript">



</script>

</asp:Content>
