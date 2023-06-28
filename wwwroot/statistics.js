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
                option.value = personData['id'];
                option.text = personData['name'];
                selectElement.add(option);
            })

            // Update the status when the selected option changes
            selectElement.onchange = function () {
                var selectedId = selectElement.options[selectElement.selectedIndex].value;
                var selectedPersonData = result.find(personData => personData['id'] == selectedId);
                statusElement.textContent = "Current Status: " + selectedPersonData['status'];

                // Load the report for the selected person
                loadReport(selectedId);
            }

            // Update the status based on the initially selected option
            var initialSelectedId = selectElement.options[selectElement.selectedIndex].value;
            var initialSelectedPersonData = result.find(personData => personData['id'] == initialSelectedId);
            statusElement.textContent = "Current Status: " + initialSelectedPersonData['status'];

            // Load the report for the initially selected person
            loadReport(initialSelectedId);
        } else {
            console.error("Error loading data: " + request.status + " " + request.statusText);
        }
    }

    request.onerror = () => {
        console.error("Request failed");
    };

    request.send();
}

function loadReport(id) {
    const url = apiAddress + '/api/report/Id?Id=' + id;
    const request = new XMLHttpRequest();

    request.open('GET', url);
    request.onload = () => {
        if (request.status == 200) {
            var result = JSON.parse(request.response);

            var checkInTimesBody = document.getElementById('checkInTimesBody');
            var checkOutTimesBody = document.getElementById('checkOutTimesBody');

            // Clear any existing rows
            checkInTimesBody.innerHTML = '';
            checkOutTimesBody.innerHTML = '';

            result.forEach(record => {
                var dateParts = record.date.split(' ');
                var row = document.createElement('tr');
                row.innerHTML = `<td>${dateParts[0]}</td><td>${dateParts[1]} ${dateParts[2]}</td>`;

                if (record.direction == 'Going In') {
                    checkInTimesBody.appendChild(row);
                } else {
                    checkOutTimesBody.appendChild(row);
                }
            });
        } else {
            console.error("Error loading data: " + request.status + " " + request.statusText);
        }
    }

    request.onerror = () => {
        console.error("Request failed");
    };

    request.send();
}
