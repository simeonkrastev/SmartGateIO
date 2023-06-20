function loadReportData() {
    console.log('Loading report data...');

    var content = document.getElementById('content');
    
    const url = apiAddress + '/api/report/';
    const request = new XMLHttpRequest();

    request.open('GET', url);
    request.send();

    request.onload = (event) => {
        var result = JSON.parse(request.response);

        result.forEach(checkinData => {
            //var text = "Tag: " + checkinData['rfidTag'] + ", Date: " + checkinData['date'];

            //var textNode = document.createTextNode(text);
            //var listItem = document.createElement('li');
            //listItem.appendChild(textNode);
            //content.appendChild(listItem);
            var reportTable = document.getElementById("reportTable");
            if (tag.Value != reportTable.Value) {
                var accountName = checkinData['rfidTag'];
                var dataCheckin = checkinData['data'];

                var firstItem = document.createTextNode(accountName);
                var firstItemResult = document.createElement('td');
                firstItemResult.appendChild(firstItem);
                content.appendChild(firstItemResult);

                var secondItem = document.createTextNode(dataCheckin);
                var secondItemResult = document.createElement('td');
                secondItemResult.appendChild(secondItem);
                content.appendChild(secondItemResult);
            }
        })
    }
}