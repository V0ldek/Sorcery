if (localStorage.getItem('sorcery-theme') === 'light') {
    const stylesheet = document.getElementById('mud-light-stylesheet-link');
    stylesheet.removeAttribute('disabled');
} else {
    const stylesheet = document.getElementById('mud-dark-stylesheet-link');
    stylesheet.removeAttribute('disabled');
}