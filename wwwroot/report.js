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
            var text = "Tag: " + checkinData['rfidTag'] + ", Date: " + checkinData['date'];

            var textNode = document.createTextNode(text);
            var listItem = document.createElement('li');
            listItem.appendChild(textNode);
            content.appendChild(listItem);
        })
    }
}