

window.onload = function loadReportData() {
    console.log('Loading report data...');

    const url = apiAddress + '/api/accounts';
    const request = new XMLHttpRequest();

    request.open('GET', url);
    request.send();

    request.onload = (event) => {
        var result = JSON.parse(request.response);

        var selectElement = document.getElementById('names');

        // Clear existing options
        selectElement.innerHTML = '';

        result.forEach(personData => {
            var option = document.createElement('option');
            option.value = personData['name'];
            option.text = personData['name'];
            selectElement.add(option);
        })
    }
}
