function checkUserRole(role, expectedRole) {
    if (role !== expectedRole) {
        // Redirect users to a different page, like an error or login page
        window.location.href = '/auth/login';
    }
    else {
        var settingsHtml = `
                <div class="">
                    <button class="btn dropdown-toggle d-flex align-items-center pt-2" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <i class='bx bx-sm bx-user mr-2'></i><span>` + sessionStorage.getItem('usrName') + `</span>
                    </button>
                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">
                        <a class="dropdown-item" id="logout">Logout</a>
                    </div>
                </div>
                    `;
        $('#settings-buttons').html(settingsHtml);
    }
}
function logOut() {
    // Attach click event handler to the logout button
    $("#logout").off('click').on('click', function () {
        // Clear sessionStorage (you can also use localStorage if you store data there)
        // sessionStorage.clear(); // This will remove all items from sessionStorage

        // Optionally, you can also clear specific items like this:
        sessionStorage.removeItem("token");
        sessionStorage.removeItem("role");
        sessionStorage.removeItem("usrId");
        sessionStorage.removeItem("usrName");

        // Redirect to the login page (or another page, such as a logout confirmation)
        window.location.href = '/auth/login'; // Adjust this path as necessary
    });
}

function highlightNavMenu() {
    // Get the current URL path
    var currentUrl = window.location.pathname;

    // Find the corresponding <a> element with the href attribute matching the current URL
    var currentLink = $('.slide-menu a[href="' + currentUrl + '"]');

    // Add the active class based on the current URL
    if (currentLink.closest('ul').hasClass('sub-menu')) {
        currentLink.closest('li.sub-active').addClass('active');
    } else {
        currentLink.closest('li').addClass('active');
    }

    // Handle click events on <a> elements
    $('.slide-menu a').off('click').on('click', function (e) {
        var clickedLiTab = $(this).closest('li');

        // Check if the clicked <a> is inside a <li> with class noactive
        if (!clickedLiTab.hasClass('noactive')) {
            // Prevent the default action of the link
            e.preventDefault();

            // Remove the active class from all <li> elements
            $('.slide-menu li').removeClass('active');

            // Check if the clicked link is inside a sub-menu and its parent <li> has the class sub-active
            if (clickedLiTab.hasClass('sub-active')) {
                // Add active class to the <li> with sub-active class
                clickedLiTab.addClass('active');
            } else if (!clickedLiTab.hasClass('noactive')) {
                // Add active class to the <li> of the clicked <a> if it is not noactive
                clickedLiTab.addClass('active');
            }

            // Redirect to the href of the clicked link
            window.location.href = $(this).attr('href');
        }
    });
}

function formatTime(timeString, {
    includeSeconds = false,
    includeAmPm = false
} = {}) {
    // Parse hours, minutes, and seconds from the input string
    let [hours, minutes, seconds] = timeString.split(':').map(Number);

    let amPm = '';
    if (includeAmPm) {
        // Determine AM or PM
        amPm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12 || 12; // Convert to 12-hour format, with 12 instead of 0
    }

    // Format hours and minutes to always have two digits
    hours = String(hours).padStart(2, '0');
    minutes = String(minutes).padStart(2, '0');
    seconds = String(seconds).padStart(2, '0');
    if (includeSeconds) {
        // Return formatted time string
        return `${hours}:${minutes}:${seconds} ${amPm}`;
    }
    // Return formatted time string
    return `${hours}:${minutes} ${amPm}`;
};
function getTimeFromDate(date) {
    let d = new Date(date);
    // Get hours, minutes
    let hours = String(d.getHours()).padStart(2, '0');
    let minutes = String(d.getMinutes()).padStart(2, '0');

    // Optionally, get seconds
    let seconds = String(d.getSeconds()).padStart(2, '0');

    // Return formatted time string
    return `${hours}:${minutes}:${seconds}`;
};
function formatDate(date, {
    ignoreHour = false,
    addDayOfWeek = false,
    includeAmPm = false,
    monthBeforeDate = false,
    lineBreak = false,
    inputDateFormat = false // New option for YYYY-MM-DD format
} = {}) {
    let d = new Date(date);

    // Get day, month, year
    let day = String(d.getDate()).padStart(2, '0');
    let month = String(d.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    let year = d.getFullYear();

    // Get hours, minutes
    let hours = String(d.getHours()).padStart(2, '0');
    let minutes = String(d.getMinutes()).padStart(2, '0');

    // Optionally, get seconds
    let seconds = String(d.getSeconds()).padStart(2, '0');

    let amPm = '';
    if (includeAmPm) {
        amPm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12 || 12; // Convert to 12-hour format, with 12 instead of 0
    }
    hours = String(hours).padStart(2, '0'); // Pad with zero if needed

    // Optional: Get day of the week if `addDayOfWeek` is true
    let dayOfWeek = '';
    if (addDayOfWeek) {
        const daysOfWeek = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        dayOfWeek = daysOfWeek[d.getDay()] + ', ';
    }
    // Handle input format: return only `YYYY-MM-DD`
    if (inputDateFormat) {
        return `${year}-${month}-${day}`;
    }
    // Return only the date if `ignoreHour` is true
    if (ignoreHour) {
        if (lineBreak) return `${dayOfWeek}<br />${day}/${month}/${year}`;
        return `${dayOfWeek}${day}/${month}/${year}`;
    }

    if (monthBeforeDate) {
        if (lineBreak) return `${dayOfWeek}<br />${month}/${day}/${year}<br />${hours}:${minutes} ${amPm}`.trim();
        // Return the formatted date and time as `day, mm/dd/yyyy hh:mm AM/PM` if `addDayOfWeek` and `includeAmPm` are true
        return `${dayOfWeek}${month}/${day}/${year} ${hours}:${minutes} ${amPm}`.trim();
    }
    if (lineBreak) return `${dayOfWeek}<br />${day}/${month}/${year}<br />${hours}:${minutes} ${amPm}`.trim();
    // Return the formatted date and time as `day, dd/mm/yyyy hh:mm AM/PM` if `addDayOfWeek` and `includeAmPm` are true
    return `${dayOfWeek}${day}/${month}/${year} ${hours}:${minutes} ${amPm}`.trim();

    // If you want to include seconds as well, use this return:
    // return `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
};
// Helper function to convert full datetime string to yyyy-MM-dd
function toDateInputFormat(dateTimeString) {
    return dateTimeString ? dateTimeString.split('T')[0] : '';
}
// Function to display alerts
function showAlert(type, message) {
    var alertHtml = '';

    if (type === 'success') {
        alertHtml = `
                    <div class="alert alert-success alert-dismissible fade show" id="notif">
                        <i class='bx bx-message-alt-x mr-3 bx-sm'></i>
                        <strong class="mr-1">` + message + `</strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true"><i class='bx bx-x bx-sm'></i></span>
                        </button>
                    </div>
                `;
    } else if (type === 'error') {
        alertHtml = `
                    <div class="alert alert-danger alert-dismissible fade show" id="notif">
                        <i class='bx bx-message-alt-x mr-3 bx-sm'></i>
                        <strong class="mr-1">` + message + `</strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true"><i class='bx bx-x bx-sm'></i></span>
                        </button>
                    </div>
                `;
    }

    // Inject the alert HTML into the alert container
    $('#alertContainer').html(alertHtml);
}