$(document).ready(function () {

    ShowRecords();

    
    $(".addButton").click(function () {
        $(".addArea").slideToggle("fast");
    });
    $(".saveButton").click(function () {
        var name = $(".name").val();
        var check = $(".check").val();
       
            $.ajax({
                url: "/api/AddRecord",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(item),
                success: function () {
                    
                    ShowRecords();
                },
                error: function (error) {
                    console.error("Error adding item:", error);
                }
            });
        
    });
    function ShowRecords() {
        
        $.ajax({
            url: "/api/GetAllRecords",
            type: "GET",
            dataType: "json",
            success: function (data) {
                
                CreateGrid(data);
            },
            error: function (error) {
                console.error("Error loading items:", error);
            }
        });
    }

    function CreateGrid(records) {
        // Clear the existing grid content
        $("#recordGrid").empty();

        // Add table headers
        $("#recordGrid").append("<tr><th>ID</th><th>Name</th><th>Activated</th><th>Action</th></tr>");

        // Loop through the items and add rows to the grid
        records.forEach(function (record) {
            // Append a new row with item details and a delete button
            $("#recordGrid").append(
                "<tr>" +
                "<td>" + record.id + "</td>" +
                "<td>" + record.name + "</td>" +
                "<td>" + (record.activated ? "Yes" : "No") + "</td>" +
                "<td><button class='btn btn-danger deleteButton' data-id='" + record.id + "'>Delete</button></td>" +
                "</tr>"
            );
        });
    }
    function deleteRecord(recordId) {
        $.ajax({
            url: "/api/DeleteRecord/" + recordId,
            type: "DELETE",
            success: function () {

                ShowRecords();
            },
            error: function (error) {
                console.error("Error deleting item:", error);
            }
        });
    }

});




