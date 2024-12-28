(function() {
  // Method signature matching $.fn.each()'s, for easy use in the .each loop later.
  var initialize = function(i, el) {
    // el is the input element that we need to initialize a map for, jQuery-ize it,
    //  and cache that since we'll be using it a few times.
    var $input = $(el);

    // Create the map div and insert it into the page.
    var $map = $('<div>', {
      css: {
        width: '400px',
        height: '400px'
      }
    }).insertAfter($input);

    // Attempt to parse the lat/long coordinates out of this input element.
    var latLong = parseLatLong(this.value);

    // If there was a problem attaining a lat/long from the input element's value,
    //  set it to a sensible default that isn't in the middle of the ocean.
    if (!latLong || !latLong.latitude || !latLong.longitude) {
      latLong = {
        latitude: 40.716948,
        longitude: -74.003563
      };
    }

    // Create a "Google(r)(tm)" LatLong object representing our DBGeometry's lat/long.
    var position = new google.maps.LatLng(latLong.latitude, latLong.longitude);

    // Initialize the map widget.
    var map = new google.maps.Map($map[0], {
      zoom: 14,
      center: position,
      mapTypeId: google.maps.MapTypeId.ROADMAP,
      maxZoom: 14
    });

    // Place a marker on it, representing the DBGeometry object's position.
    var marker = new google.maps.Marker({
      position: position,
      map: map
    });

    var updateMarker = function(updateEvent) {
      marker.setPosition(updateEvent.latLng);

      // This new location might be outside the current viewport, especially
      //  if it was manually entered. Pan to center on the new marker location.
      map.panTo(updateEvent.latLng);

      // Black magic, courtesy of Hanselman's original version.
      $input.val(marker.getPosition().toUrlValue(13));
    };

    // If the input came from an EditorFor, initialize editing-related events.
    if ($input.hasClass('editor-for-dbgeography')) {
      google.maps.event.addListener(map, 'click', updateMarker);

      // Attempt to react to user edits in the input field.
      $input.on('change', function() {
        var latLong = parseLatLong(this.value);

        latLong = new google.maps.LatLng(latLong.latitude, latLong.longitude);

        updateMarker({ latLng: latLong });
      });
    }
  };

  var parseLatLong = function(value) {
    if (!value) { return undefined; }

    var latLong = value.match(/-?\d+\.\d+/g);

    return {
      latitude: latLong[0],
      longitude: latLong[1]
    };
  };

  // Find all DBGeography inputs and initialize maps for them.
  $('.editor-for-dbgeography, .display-for-dbgeography').each(initialize);
})();