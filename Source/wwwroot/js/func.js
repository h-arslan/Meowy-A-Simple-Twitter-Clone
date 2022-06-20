
function checkUser() {
    var username = document.getElementById('myUsername').value;
    var password = document.getElementById('myPassword').value;

    if (username.length == "" && password.length == "") {
        alert("User Name and Password fields are empty");
        return false;
    }
    else {
        if (username.length == "") {
            alert("User Name is empty");
            return false;
        }
        if (password.length == "") {
            alert("Password field is empty");
            return false;
        }
    }
}

function delete_btn(id) {
    alert(id);
}
