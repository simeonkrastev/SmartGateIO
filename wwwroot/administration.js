document.getElementById('add-account-form').addEventListener('submit', function (event) {
    var name = document.getElementById('name').value;
    var cardTag = document.getElementById('card-tag').value;

    var account = {
        name: name,
        cardTag: cardTag
    };

    fetch('/api/accounts', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(account)
    });

    event.preventDefault();
});

document.getElementById('delete-account-form').addEventListener('submit', function (event) {
    var accountId = document.getElementById('account-id').value;

    fetch('/api/accounts/' + accountId, {
        method: 'DELETE'
    });

    event.preventDefault();
});