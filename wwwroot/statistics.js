window.onload = function loadReportData() {
    console.log('Loading statistics data...');

    const url = apiAddress + '/api/accounts';
    const request = new XMLHttpRequest();

    request.open('GET', url);
    console.log("Request opened");
    request.onload = () => {
        if (request.status == 200) {
            console.log("Response received with status 200");
            var result = JSON.parse(request.response);
            var selectElement = document.getElementById('names');
            var statusElement = document.getElementById('status');

            // Clear existing options
            selectElement.innerHTML = '';

            console.log(request.response);

            result.forEach(personData => {
                var option = document.createElement('option');
                option.value = personData['name'];
                option.text = personData['name'];
                selectElement.add(option);
            })

            // Update the status when the selected option changes
            selectElement.onchange = function () {
                var selectedName = selectElement.options[selectElement.selectedIndex].value;
                var selectedPersonData = result.find(personData => personData['name'] === selectedName);
                statusElement.textContent = "Current Status: " + selectedPersonData['status'];
            }

            // Update the status based on the initially selected option
            var initialSelectedName = selectElement.options[selectElement.selectedIndex].value;
            var initialSelectedPersonData = result.find(personData => personData['name'] === initialSelectedName);
            statusElement.textContent = "Current Status: " + initialSelectedPersonData['status'];
        } else {
            console.error("Error loading data: " + request.status + " " + request.statusText);
        }
    }

    request.onerror = () => {
        console.error("Request failed");
    };

    request.send();
}
