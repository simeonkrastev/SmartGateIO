function loadReportData() {
    console.log('Loading report data...');

    var content = document.getElementById('content');
    document.getElementById("dateInput").addEventListener("change", filterTableDate);
    
    const url = apiAddress + '/api/report/';
    const request = new XMLHttpRequest();

    request.open('GET', url);
    request.send();

    request.onload = (event) => {
        var result = JSON.parse(request.response);

        result.forEach(checkinData => {

            //table

            var accountName = checkinData['name'];
            var dateCheckin = checkinData['date'];
            var direction = checkinData['direction']; 
            var validation = checkinData['validationStatus']; 
            var tableBody = document.getElementById('tableBody');

            var newRow = document.createElement('tr');
            newRow.classList.add('data-row');

            var accountNameCell = document.createElement('td');
            var accountNameText = document.createTextNode(accountName);
            accountNameCell.appendChild(accountNameText);
            newRow.appendChild(accountNameCell);

            var dateCell = document.createElement('td');
            var dateText = document.createTextNode(dateCheckin);
            dateCell.appendChild(dateText);
            newRow.appendChild(dateCell);

            var directionCell = document.createElement('td');
            var directionText = document.createTextNode(direction);
            directionCell.appendChild(directionText);
            newRow.appendChild(directionCell);

                if (validation == true) {
                    validation = "Valid";
                    var validationCell = document.createElement('td');
                    var validationText = document.createTextNode(validation);
                    validationCell.appendChild(validationText);
                    newRow.appendChild(validationCell);
                    validation = "";
                    validationCell.style.color = "green";
                }
                else if (validation == false) {
                    validation = "Not Valid";
                    var validationCell = document.createElement('td');
                    var validationText = document.createTextNode(validation);
                    validationCell.appendChild(validationText);
                    newRow.appendChild(validationCell);
                    validation = "";
                    validationCell.style.color = "red";
            }

            if (direction === "Going In") {
                directionCell.style.color = "green";
            } else if (direction === "Going Out") {
                directionCell.style.color = "goldenrod";
            } else {
                directionCell.style.color = "red";
            }

            tableBody.appendChild(newRow);
        })
    }
}

    //tableFilter
function filterTable() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("input");
    filter = input.value.toUpperCase();
    table = document.getElementById("reportTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function filterTableDate() {
    var input, filter, table, tr, td, i;
    input = document.getElementById("dateInput");
    filter = new Date(input.value);
    table = document.getElementById("reportTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            var cellDate = new Date(td.innerText || td.textContent);
            var cellDateString = cellDate.toISOString().split("T")[0]; 
            var filterDateString = filter.toISOString().split("T")[0];

            if (cellDateString <= filterDateString) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function filterTableDirection() {
    var select, filter, table, tr, td, i;
    select = document.getElementById("directionFilter");
    filter = select.value;
    table = document.getElementById("reportTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[2];
        if (td) {
            if (filter === "" || td.innerHTML === filter) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function filterTableValidation() {
    var select, filter, table, tr, td, i;
    select = document.getElementById("validationFilter");
    filter = select.value;
    table = document.getElementById("reportTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[3];
        if (td) {
            if (filter === "" || td.innerHTML === filter) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}