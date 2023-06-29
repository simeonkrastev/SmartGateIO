document.getElementById('add-account-form').addEventListener('submit', function (event) {
    event.preventDefault();

    var name = document.getElementById('name').value;
    var cardTag = document.getElementById('card-tag').value;

    var account = {
        name: name,
        cardTag: cardTag
    };

    fetch('/api/accounts/' + name + '/' + cardTag, {
        method: 'POST'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}, status text: ${response.statusText}`);
            }
            // Possibly update UI here to indicate success
        })
        .catch(error => console.error('There has been a problem with your fetch operation:', error));
});

document.getElementById('delete-account-form').addEventListener('submit', function (event) {
    event.preventDefault();

    var accountId = document.getElementById('account-id').value;

    fetch('/api/accounts/Id?Id=' + accountId, {
        method: 'DELETE'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}, status text: ${response.statusText}`);
            }
            // Possibly update UI here to indicate success
        })
        .catch(error => console.error('There has been a problem with your fetch operation:', error));
});
