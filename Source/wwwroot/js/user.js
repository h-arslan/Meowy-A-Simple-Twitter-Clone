const uri = 'api/User';

function getUsers() {
    fetch(uri)
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addUser() {
    const name = document.getElementById('add-name');
    const surname = document.getElementById('add-surname');
    const username = document.getElementById('add-username');
    const email = document.getElementById('add-email');
    const password = document.getElementById('add-password');

    const item = {
        name: name.value.trim(),
        surname: surname.value.trim(),
        username: username.value.trim(),
        email: email.value.trim(),
        password: password.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getUsers();
            name.value = '';
            surname.value = '';
            username.value = '';
            email.value = '';
            password.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

//function deleteUser(id) {
//    fetch(`${uri}/${id}`, {
//        method: 'DELETE'
//    })
//        .then(() => location.reload())
//        .catch(error => console.error('Unable to delete item.', error));
//}

function updateUser(id) {
    const item = {
        name: document.getElementById('update-name').value.trim(),
        surname: document.getElementById('update-surname').value.trim(),
        username: document.getElementById('update-username').value.trim(),
        email: document.getElementById('update-email').value.trim(),
        password: document.getElementById('update-password').value.trim(),
    };

    fetch(`${uri}/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => console.log(id))
        .catch(error => console.error('Unable to update item.', error));
}