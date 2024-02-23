// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*window.setMobileTable = function (selector) {
    // if (window.innerWidth > 600) return false;
    const tableEl = document.querySelector(selector);
    const thEls = tableEl.querySelectorAll('thead th');
    const tdLabels = Array.from(thEls).map(el => el.innerText);
    tableEl.querySelectorAll('tbody tr').forEach(tr => {
        Array.from(tr.children).forEach(
            (td, ndx) => td.setAttribute('label', tdLabels[ndx])
        );
    });
}*/


function requestConfirmation(event) {
    event.preventDefault();
    swal.fire({
        title: "Are you sure you want to update?",
        icon: "question",
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: "Save",
        denyButtonText: "Don't save"
    }).then(result => {
        if (result.isConfirmed) {
            const updateForm = document.getElementById('updateForm');
            updateForm.submit();
            Swal.fire("Saved!", "Saved with the new changes!", "success");
        }
        else if (result.isDenied) {
            swal.fire("Cancelled!", "Update operation not performed", "info");
        }
    });
}
