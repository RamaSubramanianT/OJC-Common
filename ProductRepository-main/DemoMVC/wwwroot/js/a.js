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