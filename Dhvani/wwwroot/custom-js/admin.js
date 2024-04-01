

function generateApiKeyToken(userId) {
    $.ajax({
        url: '/Api/User/generate-token',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(userId),
        success: function (data) {
            //alert(data.token) 
            console.log("Received data:", data);
            $('#apiKeyLabel').html(data.token);

        },
        error: function () {
            // Handle error
            console.log('Error occurred while generating API key token.');
        }
    });
}

// send the selected dropdown value to the controller and get the data from the controller and display the second drop down value and fill those value in second drop down
function getDropDownValues() {
    $.ajax({
        url: '/Api/User/GetDropDownValues',
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {
            console.log("Received data:", data);
            populateDropDown(data);
        },
        error: function () {
            // Handle error
            console.log('Error occurred while fetching dropdown values.');
        }
    });
}


function populateDropDown(data) {
    // Assuming data is an array of objects with localName property
    var select = $('#languageselect'); // Assuming the ID of your dropdown is languageselect
    select.empty(); // Clear previous options
    select.append($('<option></option>').attr('value', '').text('Select'));
    $.each(data, function (index, item) {
        // Append option to dropdown
        select.append($('<option></option>').attr('value', item.locale).text(item.localeName));
    });
}

// =====================
// First dropdown End here
// =====================

// Assuming the first dropdown ID is 'languageselect' and the second dropdown ID is 'voiceselect'
$('#languageselect').change(function () {
    var selectedLanguage = $(this).val();
    getRelatedValues(selectedLanguage); // Call function to fetch related values
});

function getRelatedValues(selectedLanguage) {
    $.ajax({
        url: '/Api/User/GetLanguageRelatedValues',
        type: 'GET',
        data: { language: selectedLanguage }, // Pass selected language as parameter
        contentType: 'application/json',
        success: function (data) {
            console.log("Received related values:", data);
            populateRelatedValues(data); // Call function to populate related values in the second dropdown
        },
        error: function () {
            // Handle error
            console.log('Error occurred while fetching related values.');
        }
    });
}

function populateRelatedValues(data) {
    var select = $('#voiceselect'); // Assuming the ID of your second dropdown is 'voiceselect'
    select.empty(); // Clear previous options

    // Check if the dropdown is empty
    if (select.children().length === 0) {
        // Append a default option with some text (replace 'Select a value' with your desired default text)
        select.append($('<option></option>').attr('value', '').text('Select a value').prop('selected', true));
    }

    $.each(data, function (index, item) {
        var optionText = item.displayName + ' (' + item.voiceType + ')';
        select.append($('<option></option>').attr('value', item.shortName).text(optionText));
    });
}


//function populateRelatedValues(data) {
//    var select = $('#voiceselect'); // Assuming the ID of your second dropdown is 'voiceselect'
//    select.empty(); // Clear previous options

//    $.each(data, function (index, item) {
//       /* var optionText = item.displayName + ' - ' + item.voiceType;*/
//        var optionText = item.displayName + ' (' + item.voiceType + ')';

//        select.append($('<option></option>').attr('value', item.shortName).text(optionText));
//    });
//}






