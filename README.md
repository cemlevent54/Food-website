# Food Website Project

This project is a web application for managing and displaying food recipes. It includes various features such as user registration, login, and different user interfaces for admins and regular users. The project uses ASP.NET Web Forms, C#, and SQL Server Management Studio.

## Features

**Admin Page:** Admins can manage categories, approve or disapprove recipes, and add new recipes.

**Home Page:** Displays the recipes categorized by type and provides navigation to different sections like daily recipes and recipe suggestions.

**Login Page:** Allows users to log in with their credentials.

**Sign-Up Page:** New users can register by providing necessary details like name, username, password, etc.

**Recipe Details Page:** Displays detailed information about a specific recipe, including ingredients, instructions, and user comments.

## Home Page

![anasayfa](https://github.com/user-attachments/assets/ae0a081a-80e3-43b1-897a-7df2bb34c0ed)

When you run the application, the home page appears. There are `"Log In"` and `"Sign Up"` buttons at the top right corner. Other sections on the home page include:
![girisyap](https://github.com/user-attachments/assets/a92c0273-0d18-4850-affe-c27817e41185)
![uyeol](https://github.com/user-attachments/assets/079ff884-ae4a-48b4-bb98-509a7e9ce2f5)
**Dish of the Day:** Recipes selected and recommended by the admin.

**Suggest a Recipe:** A section where users can suggest new recipes.

**About Us:** Information about the project and the team.

**Contact:** Contact information and form.

**Categories:** A list of different food categories.

**Recipe Details:**
When you click on a recipe, detailed information about the recipe and a comment section will appear. The dish of the day and approved recipes are selected from the admin page.
![yemekdetaysayfası](https://github.com/user-attachments/assets/391f2b90-cd03-4baa-afde-ca6b316c2b2f)

**User Login:**
When you click the Log In button, if the user's status is 'normal', they are redirected to the home page, and if 'Admin', they are redirected to the admin page. This feature can be set from the admin page.

## Admin Page

![adminsayfasi](https://github.com/user-attachments/assets/24f75321-dacd-4474-abee-6a40824740f3)

The admin page includes various sections for managing and approving content:

**Approving Recipes:**
Recipes from the home page are saved as unapproved in the "Recipes" section of the admin page. Once approved by the admin, these recipes move to the "Foods" section. If further approved, the recipes are displayed on the home page.

**Dish of the Day:**
In the "Dish of the Day" section on the admin page, approved recipes are displayed. From here, the dish of the day can be selected.

**Categories:**
This section includes adding, deleting, and updating categories.

**Dishes:**
This section allows for configuring dishes to be displayed on the homepage and adding new dishes.

**Comments:**
This section is for approving and viewing comments made on dishes.

**Messages:**
This section is for viewing messages sent from the contact section.

**About Us:**
This section is for updating the text in the About Us section.

**Users:**
This section allows viewing and updating the information of users registered in the system.


# Future Updates
- 
