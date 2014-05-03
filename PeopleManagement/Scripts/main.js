/// <reference path='knockout-3.1.0.js' />
/// <reference path='data-utils.js' />

$(function () {

    // Initialize Dialog and DatePicker    
    $('#edit-person-dialog').hide();
    $('#edit-person-birthday-picker').datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: 'c-160:c+10',
        dateFormat: 'mm/dd/yy',
        altFormat: 'mm/dd/yy'
    });  

    // Create a global viewModel
    var viewModel = {
        pageIndex: ko.observable(1),                        // page index of the grid
        pageSize: 5,                                        // size of one page
        pageCount: ko.observable(0),                        // total number of pages
        people: ko.observableArray([]),                     // list of current visible people
        editedPerson: ko.observable({                       // field for keeping being edited/added person
            PersonId: ko.observable(0),
            FirstName: ko.observable(''),
            LastName: ko.observable(''),
            Birthday: ko.observable(null),
            Phone: ko.observable(''),
            Email: ko.observable(''),
            LastModified: ko.observable(null)
        }),

        // Function for loading data from server
        loadData: function () {
            PMUtils.loadPeopleList(this);
        },

        // Function for navigating to next page
        nextPage: function () {            
            var currIndex = this.pageIndex();
            if (currIndex < this.pageCount()) {
                this.pageIndex(currIndex + 1);
                this.loadData();
            }
        },

        // Function for navigating to previous page
        previousPage: function () {
            var currIndex = this.pageIndex();
            if (currIndex > 1) {
                this.pageIndex(currIndex - 1);
                this.loadData();
            }
        },
        
        // Function for adding a new person
        addPerson: function () {
            var self = this;

            // Clear editedPerson field for holding new data
            PMUtils.clearPersonInfo(self.editedPerson());

            // Pop-up Person editting dialog
            $('#edit-person-dialog').dialog({
                resizable: false,
                width: 500,
                height: 500,
                modal: true,
                buttons: {
                    'Add': function () { 
                            // Add the new person
                            PMUtils.addNewPerson(self.editedPerson(), 
                            function (data) { // On Success
                                // Clear editedPerson field
                                PMUtils.clearPersonInfo(self.editedPerson());

                                // Reload grid
                                self.loadData();

                                // Close dialog
                                $('#edit-person-dialog').dialog('close');
                            },
                            function (xhr) { // On Fail
                                // Parse the respnse body
                                var response = eval('(' + xhr.responseText + ')');

                                // Get adn display error message
                                var errorMessage = PMUtils.getErrorMessage(response.ModelState);
                                alert(errorMessage);
                            });                                               
                    },                   
                    Cancel: function () {
                        // Clear editedPerson field and close dialog
                        PMUtils.clearPersonInfo(self.editedPerson());
                        $(this).dialog('close');
                    }
                }
            });
        },
    };
  
    // Computed observable for deciding which classes should be applied on next page button
    viewModel.nextButtonClass = ko.computed(function () {
        var notLastPage = viewModel.pageIndex() < viewModel.pageCount();
        return notLastPage ? 'next' : 'next disabled';
    });

    // Computed observable for deciding which classes should be applied on previous page button
    viewModel.previousButtonClass = ko.computed(function () {
        var notFirstPage = viewModel.pageIndex() > 1;
        return notFirstPage ? 'previous' : 'previous disabled';
    });

    // Wire click handler to edit button of each grid item
    $(document).on('click', '.tag-edit', function () {
        // Get the clicked grid item
        var item = ko.dataFor(this);

        // Copy grid item data to editedPerson field
        viewModel.editedPerson().PersonId(item.PersonId());
        viewModel.editedPerson().FirstName(item.FirstName());
        viewModel.editedPerson().LastName(item.LastName());
        viewModel.editedPerson().Birthday(item.Birthday());
        viewModel.editedPerson().Email(item.Email());
        viewModel.editedPerson().Phone(item.Phone());
        viewModel.editedPerson().LastModified(item.LastModified());

        // Open Person editting dialog
        $('#edit-person-dialog').dialog({
            resizable: false,
            width: 500,
            height: 500,
            modal: true,
            buttons: {
                'Save': function () {
                    // Update person information
                    PMUtils.updatePerson(viewModel.editedPerson(),
                                        function (data) { // On success
                                            // Update new data back to grid item
                                            PMUtils.updatePersonInfo(item, data);

                                            // Clear editedPerson field and close dialog
                                            PMUtils.clearPersonInfo(viewModel.editedPerson());
                                            $('#edit-person-dialog').dialog('close');
                                        },
                                        function (xhr) {
                                            // Parse response body
                                            var response = eval('(' + xhr.responseText + ')');

                                            if (response.ModelState) {
                                                // Show model error
                                                var errorMessage = PMUtils.getErrorMessage(response.ModelState);
                                                alert(errorMessage);
                                            } else {
                                                // Unknown error, it could be concurrency problem
                                                alert('Error while updating given person');
                                            }
                                        });
                },
                Cancel: function () {

                    // Clear editedPerson field and close dialog
                    PMUtils.clearPersonInfo(viewModel.editedPerson());
                    $(this).dialog('close');
                }
            }
        });
    });

    // Wire click handler for remove button of each grid item
    $(document).on('click', '.tag-remove', function () {
        // Confirm before delete
        var r = confirm('Are you sure to delete this Person?');

        if (r == true) {
            // Get selected grid item
            var item = ko.dataFor(this);

            // Remove person fron database
            PMUtils.removePerson(item,
                function (data) { // On success
                    // Reload person list
                    viewModel.loadData();
                },
                function (xhr) { // On Fail
                    // Unknown error, could concurrency error
                    alert('Error while deleting given person');
                });
            
        }
    });

    // Load grid data for the first time.
    viewModel.loadData();

    // Apply viewModel
    ko.applyBindings(viewModel);
});