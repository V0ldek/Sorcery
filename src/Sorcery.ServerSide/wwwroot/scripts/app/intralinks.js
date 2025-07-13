// Hook intralink click handlers.
document.addEventListener("DOMContentLoaded", (event) => {
    let currentIntralinkFocus = null;

    for (let intralink of document.getElementsByClassName("sorcery-footnote-intralink")) {
        const container = document.getElementById(intralink.dataset.targetId);
        intralink.addEventListener("click", (event) => {
            container.scrollIntoView({block: "center"});
            container.focus();
            container.setAttribute('data-intralink-focused', "");

            if (currentIntralinkFocus)
            {
                currentIntralinkFocus.removeAttribute('data-intralink-focused');
            }

            currentIntralinkFocus = container;
        })
    }
});