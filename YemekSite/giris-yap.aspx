<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="giris-yap.aspx.cs" Inherits="YemekSite.giris_yap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Giriş Yap</title>

    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .image-container {
            margin-bottom: 20px;
            text-align: center;
        }

        .login-image {
            width: 300px; /* İstediğiniz boyuta göre ayarlayabilirsiniz */
            height: auto;
        }

        .login-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            width: 300px;
            text-align: center;
        }

            .login-container h2 {
                margin-bottom: 20px;
                color: #333;
            }

            .login-container form {
                display: flex;
                flex-direction: column;
            }

        .input-field {
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }

        .btn {
            padding: 10px;
            border: none;
            border-radius: 4px;
            background-color: indianred;
            color: white;
            font-size: 16px;
            cursor: pointer;
        }

            .btn:hover {
                background-color: red;
            }

        .additional-options {
            margin-top: 20px;
        }

            .additional-options a {
                color: #007bff;
                text-decoration: none;
            }

                .additional-options a:hover {
                    text-decoration: underline;
                }

            .additional-options p {
                margin: 10px 0 0;
            }
        .file-upload-status {

        }
    </style>
     <script type="text/javascript">
         function setPasswordMode() {
             var passwordField = document.getElementById('<%= password.ClientID %>');
             passwordField.setAttribute('type', 'password');
         }
         window.onload = setPasswordMode;
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="image-container">
                <asp:Image ID="loginImage" runat="server" ImageUrl="~/images/giris_resim.jpg" AlternateText="Login Image" CssClass="login-image" />
            </div>
            <h2>Giriş Yap</h2>
            <asp:TextBox ID="username" runat="server" placeholder="Kullanıcı Adı" CssClass="input-field"></asp:TextBox>
            <asp:TextBox ID="password" runat="server" placeholder="Şifre" CssClass="input-field" ></asp:TextBox><br />

            <div class="remember_me">
                <asp:CheckBox ID="rememberMe" runat="server" />
                <label for="rememberMe">Beni Hatırla</label>
            </div>
            <br />
            <asp:Button ID="loginButton" runat="server" Text="Giriş Yap" CssClass="btn" OnClick="loginButton_Click" /> 
            <br /> <br />
            <asp:Label ID="uploadStatusLabel" runat="server" CssClass="file-upload-status" Text=""></asp:Label>

            <div class="additional-options">
                <asp:LinkButton ID="linkLbl_sifre" runat="server" OnClick="linkLbl_sifre_Click">Şifremi Unuttum</asp:LinkButton>
                <p>
                    Üye değil misiniz? Hızlıca 
                   
                    <asp:LinkButton ID="linkLbl_uyeOl" runat="server" OnClick="linkLbl_uyeOl_Click">Üye Olun</asp:LinkButton>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
