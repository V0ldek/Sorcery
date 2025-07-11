// Hook intralink click handlers.
document.addEventListener("DOMContentLoaded", (event) => {
    const intralinks = document.getElementsByClassName("sorcery-footnote");
    let currentIntralinkFocus = null;

    for (let intralink of intralinks) {
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