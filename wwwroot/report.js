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
            var direction = checkinData['status']; 
            var validation = checkinData['validationStatus']; 
            var tableBody = document.getElementById('tableBody');

            var newRow = document.createElement('tr');

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

            if (direction === "IN") {
                directionCell.style.color = "green";
            } else if (direction === "OUT") {
                directionCell.style.color = "darkyellow";
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
    filter = input.value;
    table = document.getElementById("reportTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            if (td.innerHTML <= filter) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
