/// <reference path="knockout-3.1.0.js" />

$(function () {

    // Create a new namespace for People Management Utility
    window.PMUtils = window.PMUtils || {};

    var ns = window.PMUtils;

    // Conberts Person object to observable
    ns.toObservable = function (person) {      
        var koPerson = {
            PersonId: ko.observable(person.PersonId),
            FirstName: ko.observable(person.FirstName),
            LastName: ko.observable(person.LastName),
            Birthday: ko.observable(person.Birthday),
            Email: ko.observable(person.Email),
            Phone: ko.observable(person.Phone),
            LastModified: ko.observable(person.LastModified),
        }

        return koPerson;
    };

    // Loads people list for given view-model 
    ns.loadPeopleList = function (vm) {
        var url = '/api/PeopleApi?page=' + (vm.pageIndex() - 1) + '&size=' + vm.pageSize;
        $.get(url)
        .done(function (data) {

            // Set pageCount
            vm.pageCount(data.PageCount);

            // Clear people collection
            vm.people.removeAll();

            // Push new data into people collection
            for (var index in data.People) {
                var koPerson = PMUtils.toObservable(data.People[index]);
                vm.people.push(koPerson);
            }
        })
        .fail(function (xhr, statusText) {
            alert('Error while loading people list : ' + statusText);
        });
    };

    // Gets model error's messages
    ns.getErrorMessage = function (modelState) {
        var message = "";
        if (modelState.hasOwnProperty("person.FirstName")) {
            message += "FirsName: " + modelState["person.FirstName"] + "\n";
        }
        if (modelState.hasOwnProperty("person.LastName")) {
            message += "LastName: " + modelState["person.LastName"] + "\n";
        }
        if (modelState.hasOwnProperty("person.Birthday")) {
            message += "Birthday: " + modelState["person.Birthday"] + "\n";
        }
        if (modelState.hasOwnProperty("person.Email")) {
            message += "Email: " + modelState["person.Email"] + "\n";
        }
        if (modelState.hasOwnProperty("person.Phone")) {
            message += "Phone: " + modelState["person.Phone"] + "\n";
        }

        return message;
    }

    // Adds a new Person
    ns.addNewPerson = function (newPerson, onSuccess, onFail) {
        var url = '/api/PeopleApi/';        
        $.ajax({
            url: url,
            type: 'POST',
            data: {
                FirstName: newPerson.FirstName(),
                LastName: newPerson.LastName(),
                Birthday: newPerson.Birthday(),
                Email: newPerson.Email(),
                Phone: newPerson.Phone()
            },
        })
        .done(function (data) {
            onSuccess(data);
        })
        .fail(function (xhr, statusText) {
            onFail(xhr);
        });

    };

    // Updates Person's information
    ns.updatePerson = function (editedPerson, onSuccess, onFail) {
        var url = '/api/PeopleApi/' + editedPerson.PersonId();     
        $.ajax({
            url: url,
            type: 'PUT',
            data: {
                PersonId: editedPerson.PersonId(),
                FirstName: editedPerson.FirstName(),
                LastName: editedPerson.LastName(),
                Birthday: editedPerson.Birthday(),
                Email: editedPerson.Email(),
                Phone: editedPerson.Phone(),
                LastModified: editedPerson.LastModified()
            },
        })
        .done(function (data) {
            onSuccess(data);
        })
        .fail(function (xhr, statusText) {
            onFail(xhr);
        });
    };

    // Removes a Person
    ns.removePerson = function (removedPerson, onSuccess, onFail) {
        var url = '/api/PeopleApi/' + removedPerson.PersonId();
        $.ajax({
            url: url,
            type: 'DELETE',
            data: {
                PersonId: removedPerson.PersonId(),
                LastModified: removedPerson.LastModified()
            },
        })
        .done(function (data) {
            onSuccess(data);
        })
        .fail(function (xhr, statusText) {
            onFail(xhr);
        });
    }

    // Reload person information
    ns.reloadPerson = function (updatedPerson, onSuccess, onFail) {
        var url = '/api/PeopleApi/' + updatedPerson.PersonId();
        $.ajax({
            url: url,
            type: 'GET'
        })
        .done(function (data) {
            updatedPerson(updatedPerson, data)
        })
        .fail(function (xhr, statusText) {
            alert("Could not update person's information")
        });
    }

    // Cleares the person's information
    ns.clearPersonInfo = function (person) {
        person.PersonId(0);
        person.FirstName("");
        person.LastName("");
        person.Birthday(null);
        person.Email("");
        person.Phone("");
        person.LastModified(null);
    }

    // Updates given person (ko object) with raw data 
    ns.updatePersonInfo = function (person, data) {
        person.PersonId(data.PersonId);
        person.FirstName(data.FirstName);
        person.LastName(data.LastName);
        person.Birthday(data.Birthday);
        person.Email(data.Email);
        person.Phone(data.Phone);
        person.LastModified(data.LastModified);
    }
});