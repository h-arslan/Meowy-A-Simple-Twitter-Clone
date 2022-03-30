requirements-documents-template  
requirements-documents-template.md
### Meowy A Simple Twitter Clone
### Requirements Specification Document
Version <1.0.0>  
Revision History Table

#### 1.Introduction  
This document is the requirements documentation of the simple twitter clone application that needs to be developed in the CENG423 Web Application Development course. The following information is included in the requirements specification document of the application that we will develop; non-functional requirements, user stories/functional specifications, database design and mock user interface screens. As a reference -links-

#### 2.Non Functional Requirements  

- Usability:   
  * Navigation: The main goal of navigation is navigating inside the application to any page should not require to use browser’s back/forward buttons.. i.e, navgating to  a page and coming all the way back without using back/forward.  
  * Purpose of Feature: Users should be able to understand where the detailed options are by looking the general icons/buttons.  i.e Home Page to Changing of Profile   Picture image  
  
- Localization: Localization is measure of proper context display on different locations. This different locations means countries which are in different time zones and different languages spoken. In this application, users will be able to display their language symbols properly, for instance Chinese, Japanese, use the application with their time zone.  
- Security:  
	Users should obey some security rules to protect their and others accounts. These rules are,  
  *	While registring the website users should be verify their email or phone number.   
  *	While defining a password users should obey the limitations which are:  
    ---	Password should be longer than 8 characters  
    ---	Password should include at least one uppercase character and one special character  
  *	While signed-up state, user should enter his or her password to change a criticial information  about account  
  *	If a user enters a wrong password, the warning will be “Wrong username or password” to eliminate the multiple password trying.  

- Responsiveness: Display of the application will be automatically fit to the display size of computer without any misfunction.  

- Compatibility: As this application will run on web server, it will be compatible with the most of the operating systems.  

- Development and Deployment Spaces:  
  *	For backend programing and giving functionality to CRUD services, C# .NET 6, for development environment Visual Studio 2022 will be used. To read and write data by using HTTP requests, REST.API’s will be generated.  
  *	As a database server SQL SERVER and to edit and design visually, Microsoft SQL Server Management Studio will be used.   
  *	For running environment Docker containers will be used.  

#### 3.User Stories/Functional Specifications  
- The user creates his personal account with the account creation interface and logs in with his e-mail and password.  
- The user, who is logged into account, can create, delete and read the shared posts.  
- The user can filter the posts by date and the word they contain, and can also search for other users in the application.

#### 4.Database Design   

#### 5.Mock UI Screens  

#### 6.Approval/Review Date  


