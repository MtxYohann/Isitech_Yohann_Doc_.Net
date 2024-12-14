document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('eventForm');
    const validationButton = document.getElementById('validationButton');

    if (form && validationButton) {
        form.addEventListener('change', function () {
            validationButton.style.display = 'block';
        });

        // Soumission via le bouton de validation
        window.submitValidation = function () {
            form.submit();
        };
    }
});