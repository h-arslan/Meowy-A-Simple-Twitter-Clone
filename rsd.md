requirements-documents-template  
requirements-documents-template.md
### Meowy A Simple Twitter Clone
### Requirements Specification Document
Version <1.9.0>  
Revision History Table

#### 1.Introduction  
This document is the requirements documentation of the simple twitter clone application that needs to be developed in the CENG423 Web Application Development course. The following information is included in the requirements specification document of the application that we will develop; non-functional requirements, user stories/functional specifications, database design and mock user interface screens. As a reference we searched for Twitter interface coding and database scheme design. Also searched requirement document template examples.

#### 2.Non Functional Requirements  

- Usability:   
  * Navigation: The main goal of navigation is navigating inside the application to any page should not require to use browser’s back/forward buttons... i.e., navigating to  a page and coming all the way back without using back/forward.  
  * Purpose of Feature: Users should be able to understand where the detailed options are by looking the general icons/buttons. i.e., Home Page to Changing of Profile Picture image  
   
- Security:  
	Users should obey some security rules to protect their and others accounts. These rules are,
  *	While defining a password users should obey the limitations which are:  
    ---	Password should be longer than 8 characters  
    ---	Password should include at least one uppercase character and one special character  
  *	While signed-up state, user should enter his or her password to change a critical information about account  
  *	If a user enters a wrong password, the warning will be “Your username or password is wrong! Please try again...” to eliminate the multiple passwords trying.

- Compatibility: As this application will run on web server, it will be compatible with the most of the operating systems.  

- Development and Deployment Spaces:  
  *	For backend programing and giving functionality to CRUD services, C# .NET 6, for development environment Visual Studio 2022 will be used. To read and write data by using HTTP requests, REST.API’s will be generated.  
  *	As a database server SQL SERVER and to edit and design visually, Microsoft SQL Server Management Studio will be used.    

#### 3.User Stories/Functional Specifications
A brief terminology for Twitter clone we created under the name Meowy.  
Meow: Posts that users have written and shared. (tweet)   
Remeow: When users re-share post that someone else has shared (retweet)  
Comment: Posts written specifically to one of the shared post.  
Follow request: A request notification that users send to each other so they can see the content they've shared.  

User story 1: As a user, I can log in to my own account using username and password from the “sign in” page when I open the application. If I don't have an account, I can create a new account by clicking the sign-up button. The first part that comes up after logging in is the “home page”. Each page has a menu bar on the left and a small explore section on the right. I can fill in the text box in the middle of the home page, create a new post and see the posts of other users.

User story 2: As a user, when I click on the “explore” from the menu bar, I see trending topics on the page that opens. When I click on these topics, I can see the most popular posts shared on this topics. When I type the word I want to search for in the search field at the top, I can see the shared posts using this word. On the right side of each page there are small explore sections that serve the same function as this section.

User story 3: As a user, when I go to the “messages page”, I see the messages that other users send me. When I click on the chat box, I can see the message history, write new messages, and send them.

User story 4: When I enter the “follow request page” as a user, I can see the follow requests I receive. I can reject or accept these requests. The follow-follower system allows me to choose the people whose posts I want to see and the people I want to share my posts. Users who follow me can see, like and comment on my posts.

User story 5: As a user, when I click on the “profile” option in the menu bar, I can see my own posts (meows), comments, posts of other people I have shared (remeows), and posts I like. The comment, remeow and like numbers of all posts are also written at the bottom of each post. On any page, when I click on the profile image in the lower left corner, I will be redirected to my profile page.

User story 6: When I enter the “settings page” as a user, I see two parts as “account” and “security”. Account is the option that contains the exchange settings for account information. When I click on “account information”, I can change properties such as username, name, surname. When I click “change password”, I can change my password. When I click on “close account”, I can close my account.
The Security option includes the privacy settings of the account. In this section, the account can be set to private. When I change the mode to private mode, my posts can only be observed by people who follow me. when my account isn't in private mode, my posts are public.

#### 4.Database Design  
![database_diagram](https://user-images.githubusercontent.com/96079325/175096379-26e021f4-ee28-4506-82c4-fb2c235f1f3c.png)

#### 5.Mock UI Screens
>Sign-in Page
![1 signin](https://user-images.githubusercontent.com/96079325/170479437-06ca2cf1-b266-4da9-87cf-282d87818dd5.png)

>Sign-up Page
![2 signup](https://user-images.githubusercontent.com/96079325/170479482-66f32fa3-156d-4017-a27e-aed2773201e1.png)

>Home page
![3 home](https://user-images.githubusercontent.com/96079325/170479502-89bc3050-4e4e-4c53-8538-c654634cd291.png)

>Explore
![4 explore](https://user-images.githubusercontent.com/96079325/170479547-40fe5341-cbaa-46b0-b9c1-a4660c4baa01.png)

>Messages
![5 messages](https://user-images.githubusercontent.com/96079325/170479574-53116133-e3cb-43f0-90ce-0739992b6cb7.png)

>Follow requests
![6 follow](https://user-images.githubusercontent.com/96079325/170479617-18113536-97d8-4970-aca7-6c7e413d762d.png)

>Profile
![7 profile](https://user-images.githubusercontent.com/96079325/170479654-217292ad-c248-4570-93e2-2beb99a2a23c.png)

>Settings
![8 sett](https://user-images.githubusercontent.com/96079325/170479685-02c42581-3b56-4d98-ab99-e006ce28bdf5.png)

#### 6.Approval/Review Date  
31.03.2022

