let obsFields = new Array();
$('#AddField').on('click', function () {
    var fieldType = document.getElementById('fieldType');
    var fieldName = document.getElementById('fieldName');
    obsFields.push(
        { Name: fieldName.value, Type: fieldType.value }
    );
    fieldType.value = document.getElementById('typeStandardOption').value;
    fieldName.value = "";
    console.log(obsFields);
})

$("#dataSend").on('click', function () {
    var formData = new FormData();
    formData.append('Image', $('#file')[0].files[0]);
    formData.append('Title', document.getElementById('Title').value);
    formData.append('Description', document.getElementById('Description').value);
    formData.append('Topic', document.getElementById('Topic').value);
    formData.append('AdditionalFields', JSON.stringify(obsFields));

    $.ajax({
        contentType: false,
        processData: false,
        type: 'POST',
        url: '/Collection/Create',
        data: formData,
        success: function () {
            console.log("success.");
        },
        error: function () {
            console.log("error.");
        },
    });
});