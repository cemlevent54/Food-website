<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uye-ol.aspx.cs" Inherits="YemekSite.uye_ol" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Üye Ol</title>

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
        }

        .signup-image {
            width: 120px;
            height: 120px;
            object-fit: cover;
        }

        .signup-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            width: 400px;
            text-align: center;
        }

        .signup-container h2 {
            margin-bottom: 20px;
            color: #333;
        }

        .signup-container form {
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
            background-color: #5cb85c;
            color: white;
            font-size: 16px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #4cae4c;
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

        .file-upload-container {
            margin-bottom: 15px;
            display: flex;
            align-items: center;
            width: 100%;
        }

        .file-upload-buttons {
            display: flex;
            flex-direction: column;
            align-items:center;
        }

        .file-upload {
            width: 100%;
            height: 40px;
            opacity: 0;
            position: absolute;
            top: 0;
            left: 0;
            cursor: pointer;
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
        
        #txtBox_confirmPassword {
            margin-left: 200px;

        }
    </style>
    <script type="text/javascript">
        function setPasswordMode() {
            var passwordField = document.getElementById('<%= txtBox_password.ClientID %>');
            var passwordField_confirm = document.getElementById('<%= txtBox_confirmPassword.ClientID %>');
            passwordField.setAttribute('type', 'password');
            passwordField_confirm.setAttribute('type', 'password')
        }
        window.onload = setPasswordMode;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="signup-container">
            <div class="image-container">
                <asp:Image ID="signupImage" runat="server" ImageUrl="images/uye_olma_resim.png" AlternateText="Signup Image" CssClass="signup-image" />
            </div>
            <h2>Üye Ol</h2>
            <asp:TextBox ID="txtBox_isim" runat="server" placeholder="İsim" CssClass="input-field"></asp:TextBox>
            <asp:TextBox ID="txtBox_soyisim" runat="server" placeholder="Soyisim" CssClass="input-field"></asp:TextBox>
            <asp:TextBox ID="txtBox_telno" runat="server" placeholder="Telefon Numarası" CssClass="input-field"></asp:TextBox>
            <asp:TextBox ID="txtBox_email" runat="server" placeholder="Email" CssClass="input-field"></asp:TextBox>
            <asp:TextBox ID="txtBox_username" runat="server" placeholder="Kullanıcı Adı" CssClass="input-field"></asp:TextBox>
            <asp:TextBox ID="txtBox_password" runat="server" placeholder="Şifre" CssClass="input-field"></asp:TextBox>
            <asp:TextBox ID="txtBox_confirmPassword" runat="server" placeholder="Şifreyi Onayla" CssClass="input-field"></asp:TextBox>
            <div class="file-upload-container">
                <div class="file-upload-buttons">
                    <asp:FileUpload ID="profilePicture" runat="server" CssClass="file-upload" />
                    <label for="profilePicture" class="file-upload-button file-upload-select" >Fotoğraf Seç</label>
                    <asp:Button ID="uploadButton" runat="server" Text="Resmi Yükle" CssClass="file-upload-button file-upload-upload" OnClick="uploadButton_Click" />
                    <asp:Label ID="uploadStatusLabel" runat="server" CssClass="file-upload-status" Text=""></asp:Label>
                </div>
                <div class="image-box">
                    <asp:Image ID="profileImage" runat="server" CssClass="signup-image" />
                </div>
            </div>

            
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:Button ID="signupButton" runat="server" Text="Üye Ol" CssClass="btn" OnClick="signupButton_Click" />
            <div class="additional-options">
                <p>
                    Zaten bir hesabınız var mı? 
                    <asp:LinkButton ID="linkLbl_girisYap" runat="server" OnClick="linkLbl_girisYap_Click">Giriş Yap</asp:LinkButton>
                </p>
            </div>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>

