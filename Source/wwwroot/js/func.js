
function checkUser() {

    var username = document.getElementById('myUsername').value;
    var password = document.getElementById('myPassword').value;         

    if (username.length == "" || password.length == "") {
        alert("Fill the empty informations !");
        return false;
    }
    else {

        $.getJSON('http://localhost:5125/api/User', function (data) {

            $.each(data, function (key, value) {

                if (username == value.username && password == value.password) {
                    window.location.href = 'home.html';
                }
                else {
                    alert("Username or password is wrong !")
                }
            });
        });
        
    }
}
