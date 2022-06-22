
function checkUser() {    

    const username = document.getElementById('myUsername').value;
    const password = document.getElementById('myPassword').value;   

    if (username.length == "" || password.length == "") {
        alert("Fill the empty informations !");
    }    
    else {
        $.getJSON('http://localhost:5125/api/User', function (data) {

            $.each(data, function (key, value) {

                if (username == value.username && password == value.password) {
                    //arr.push(value.id);
                    window.location.href = 'home.html';
                    return false;
                }
                else {
                    alert("\nYour username or password is wrong !\nPlease try again...");
                    return false;
                }                    
            });
        });            
    }                   
}

//function getUsername() {
//    alert(arr);
//}


//const obj1 = {
//    username: 'x',
//    password: 'y'
//};

//const obj2 = obj1;

//var arr = {};

//obj2.username = document.getElementById('myUsername').value;
//obj2.password = document.getElementById('myPassword').value;