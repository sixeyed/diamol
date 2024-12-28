function NerdDinner() { }
NerdDinner.MapDivId = 'theMap';
NerdDinner._map = null;
NerdDinner.ipInfoDbKey = '';
NerdDinner.BingMapsKey = '';

NerdDinner.LoadMap = function (latitude, longitude) {
    var mapOptions = {
        credentials: NerdDinner.BingMapsKey,
        disableBirdseye: true,
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        showMapTypeSelector: false
    };

    NerdDinner._map = new Microsoft.Maps.Map(document.getElementById(NerdDinner.MapDivId), mapOptions);

    Microsoft.Maps.Events.addHandler(NerdDinner._map, 'viewchange', mapViewChange);

    if (latitude !== null && longitude !== null) {
        NerdDinner._map.setView({ center: new Microsoft.Maps.Location(latitude, longitude) });
    }
};
NerdDinner.ClearMap = function () {
    if (NerdDinner._map !== null) {
        // NerdDinner._map.entities.clear();
    }
};
NerdDinner.LoadPin = function (location, _id, _title, _description, _draggable) {
    var pinInfobox = new Microsoft.Maps.Infobox(location, { id: _id, title: _title, description: _description, visible: false });

    var pinOptions = {
        draggable: _draggable,
        infobox: pinInfobox
    };
    var pin = new Microsoft.Maps.Pushpin(location, pinOptions);

    Microsoft.Maps.Events.addHandler(pin, 'click', displayInfobox);

    NerdDinner._map.entities.push(pinInfobox);
    NerdDinner._map.entities.push(pin);
};
NerdDinner.FindMostPopularDinners = function (limit) {
    $.post("/api/Search?limit=" + limit, {}, NerdDinner._renderDinners, "json");
};
NerdDinner._renderDinners = function (dinners) {
    if (dinners == null) {
        return;
    }
    var viewModel = {
        dinners: ko.observableArray(dinners)
    };
    ko.applyBindings(viewModel);

    NerdDinner.ClearMap();

    $.each(dinners, function (i, dinner) {
        var location = new Microsoft.Maps.Location(dinner.Latitude, dinner.Longitude);

        // Add Pin to Map
        NerdDinner.LoadPin(location, dinner.DinnerID, _getDinnerLinkHTML(dinner), _getDinnerDescriptionHTML(dinner), false);
    });
};
NerdDinner.FindAddressOnMap = function (where) {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");
    script.setAttribute("src", "http://dev.virtualearth.net/REST/v1/Locations?query=" + encodeURI(where) + "&output=json&jsonp=NerdDinner._callbackForLocation&key=" + NerdDinner.BingMapsKey);
    document.body.appendChild(script);
};
NerdDinner._ZoomMap = function(result) {
    NerdDinner.ClearMap();

    if (result &&
           result.resourceSets &&
           result.resourceSets.length > 0 &&
           result.resourceSets[0].resources &&
           result.resourceSets[0].resources.length > 0) {
        // Set the map view using the returned bounding box
        var bbox = result.resourceSets[0].resources[0].bbox;
        var viewBoundaries = Microsoft.Maps.LocationRect.fromLocations(new Microsoft.Maps.Location(bbox[0], bbox[1]), new Microsoft.Maps.Location(bbox[2], bbox[3]));
        NerdDinner._map.setView({ bounds: viewBoundaries });
    }
}
NerdDinner._callbackForLocation = function (result) {
    NerdDinner._ZoomMap(result);

    if (result &&
           result.resourceSets &&
           result.resourceSets.length > 0 &&
           result.resourceSets[0].resources &&
           result.resourceSets[0].resources.length > 0) {
        // Add a pushpin at the found location
        var location = new Microsoft.Maps.Location(result.resourceSets[0].resources[0].point.coordinates[0], result.resourceSets[0].resources[0].point.coordinates[1]);
        var pushpin = new Microsoft.Maps.Pushpin(location);
        NerdDinner._map.entities.push(pushpin);

        $("#Location").val(location.latitude.toString() + "," + location.longitude.toString());
        $("#Latitude").val(location.latitude.toString());
        $("#Longitude").val(location.longitude.toString());
    }
};
NerdDinner.FindDinnersGivenLocation = function (where) {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");
    script.setAttribute("src", "http://dev.virtualearth.net/REST/v1/Locations?query=" + encodeURI(where) + "&output=json&jsonp=NerdDinner._ZoomMap&key=" + NerdDinner.BingMapsKey);
    document.body.appendChild(script);

    $.get("/api/Search?location=" + where, {}, NerdDinner._renderDinners, "json");
};

NerdDinner.onEndDrag = function (e) {
    $("#Location").val(NerdDinner._points[0].Latitude.toString() + "," + NerdDinner._points[0].Longitude.toString());
    // $("#Latitude").val(e.LatLong.Latitude.toString());
    // $("#Longitude").val(e.LatLong.Longitude.toString());
};
NerdDinner.getLocationResults = function (locations) {
    if (locations) {
        var currentAddress = $("#Address");
        if (locations[0].Name !== currentAddress) {
            var answer = confirm("Bing Maps returned the address '" + locations[0].Name + "' for the pin location. Click 'OK' to use this address for the event, or 'Cancel' to use the current address of '" + currentAddress.val() + "'");
            if (answer) {
                currentAddress.val(locations[0].Name);
            }
        }
    }
};
NerdDinner.getCurrentLocationByIpAddress = function () {
    var requestUrl = "http://api.ipinfodb.com/v3/ip-city/?format=json&callback=?&key=" + this.ipInfoDbKey;

    $.getJSON(requestUrl,
        function (data) {
            if (data.RegionName !== '') {
                // This is for the Search box
                // $('#Location').val(data.regionName + ', ' + data.countryName);
            }
        });
};
NerdDinner.getCurrentLocationByLatLong = function (latitude, longitude) {
    var requestUrl = 'http://dev.virtualearth.net/REST/v1/Locations/' + latitude + ',' + longitude + '?key=' + NerdDinner.BingMapsKey + '&jsonp=?';
    $.getJSON(requestUrl,
        function (result) {
            if (result.resourceSets[0].estimatedTotal > 0) {
                // This is for the Search box
                $('#SearchLocation').val(result.resourceSets[0].resources[0].address.formattedAddress);
            }
        });
};
ko.bindingHandlers.dateString = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);
        var pattern = allBindings.datePattern || 'MM/dd/yyyy';
        $(element).text(valueUnwrapped.toString(pattern));
    }
};
ko.bindingHandlers.rsvpMessage = {
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        var rsvpMessage = " with " + value + " RSVP";
        if (value > 1)
            rsvpMessage += "s";
        $(element).text(rsvpMessage);
    }
};
function _getDinnerDate(dinner, formatStr) {
    // return '<strong>' + _dateDeserialize(dinner.EventDate).format(formatStr) + '</strong>';
    return dinner.EventDate;
}

function _getDinnerLinkHTML(dinner) {
    return '<a href="' + dinner.Url + '">' + dinner.Title + '</a>';
}

function _getDinnerDescriptionHTML(dinner) {
    return '<p>' + _getDinnerDate(dinner, "mmmm d, yyyy") + '</p><p>' + dinner.Description + '</p>' + _getRSVPMessage(dinner.RSVPCount);
}

function _dateDeserialize(dateStr) {
    return eval('new' + dateStr.replace(/\//g, ' '));
}

function _getRSVPMessage(RSVPCount) {
    var rsvpMessage = "" + RSVPCount + " RSVP";

    if (RSVPCount > 1)
        rsvpMessage += "s";

    return rsvpMessage;
}

// This function will create an infobox 
// and then display it for the pin that triggered the hover-event.
function displayInfobox(e) {
    // build or display the infoBox
    var pin = e.target;
    if (pin != null) {
        var pinInfobox = pin._infobox;

        pinInfobox.setOptions({ visible: true });
    }
}

function displayInfobox2(e) {
    // make sure we clear any infoBox timer that may still be active
    stopInfoboxTimer(e);

    // build or display the infoBox
    var pin = e.target;
    if (pin != null) {

        // Create the info box for the pushpin
        var location = pin.getLocation();
        var options = {
            id: 'infoBox1',
            title: 'My Pushpin Title',
            description: 'This is the plain text description.',
            //htmlContent: '',
            height: 100,
            width: 150,
            visible: true,
            showPointer: true,
            showCloseButton: true,
            // offset the infobox enough to keep it from overlapping the pin.
            offset: new Microsoft.Maps.Point(0, pin.getHeight()),
            zIndex: 999
        };
        // destroy the existing infobox, if any
        // In testing, I discovered not doing this results in the mouseleave
        // and mouseenter events not working after hiding and then reshowing the infobox.
        if (pinInfobox != null) {
            map.entities.remove(pinInfobox);
            if (Microsoft.Maps.Events.hasHandler(pinInfobox, 'mouseleave'))
                Microsoft.Maps.Events.removeHandler(pinInfobox.mouseLeaveHandler);
            if (Microsoft.Maps.Events.hasHandler(pinInfobox, 'mouseenter'))
                Microsoft.Maps.Events.removeHandler(pinInfobox.mouseEnterHandler);
            pinInfobox = null;
        }
        // create the infobox
        pinInfobox = new Microsoft.Maps.Infobox(location, options);
        // hide infobox on mouseleave
        pinInfobox.mouseLeaveHandler
            = Microsoft.Maps.Events.addHandler(pinInfobox, 'mouseleave', pinInfoboxMouseLeave);
        // stop the infobox hide timer on mouseenter
        pinInfobox.mouseEnterHandler
            = Microsoft.Maps.Events.addHandler(pinInfobox, 'mouseenter', pinInfoboxMouseEnter);
        // add it to the map.
        map.entities.push(pinInfobox);
    }
}

function hideInfobox(e) {
    var pin = e.target;
    if (pin != null) {
        var pinInfobox = pin._infobox;
        if (pinInfobox != null)
            pinInfobox.setOptions({ visible: false });
    }
}

// This function starts a count-down timer that will hide the infoBox when it fires.
// This gives the user time to move the mouse over the infoBox, which disables the timer
// before it can fire, thus allowing clickable content in the infobox.
function startInfoboxTimer(e) {
    var pin = e.target;
    if (pin != null) {
        var pinInfobox = pin._infobox;
        // start a count-down timer to hide the popup.
        // This gives the user time to mouse-over the popup to keep it open for clickable-content.
        if (pinInfobox.pinTimer != null) {
            clearTimeout(pinInfobox.pinTimer);
        }
        // give 300ms to get over the popup or it will disappear
        pinInfobox.pinTimer = setTimeout(timerTriggered, 300);
    }
}

// Clear the infoBox timer, if set, to keep it from firing.
function stopInfoboxTimer(e) {
    var pin = e.target;
    if (pin != null) {
        var pinInfobox = pin._infobox;
        if (pinInfobox != null && pinInfobox.pinTimer != null) {
            clearTimeout(pinInfobox.pinTimer);
        }
    }
}

function mapViewChange(e) {
    stopInfoboxTimer(e);
    hideInfobox(e);
}
function pinMouseOver(e) {
    displayInfobox(e);
}
function pinMouseOut(e) {
    // TODO: detect if the mouse is already over the infoBox
    //  This can happen when the infobox is shown overlapping the pin where the mouse is at
    //    In that case, we shouldn't start the timer.
    startInfoboxTimer(e);
}
function pinInfoboxMouseLeave(e) {
    hideInfobox(e);
}
function pinInfoboxMouseEnter(e) {
    // NOTE: This won't fire if showing infoBox ends up putting it under the current mouse pointer.
    stopInfoboxTimer(e);
}
function timerTriggered(e) {
    hideInfobox(e);
}
