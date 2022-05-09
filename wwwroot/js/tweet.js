const uri = 'api/tweets';

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Unable to get items.', error));
}


function addTweet() {
    const contents = document.getElementById('meow_input');

    const item = {
        contents: contents.value.trim()
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
            getItems();
            contents.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

