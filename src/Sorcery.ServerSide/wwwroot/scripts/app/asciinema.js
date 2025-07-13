// Trigger Asciinema players with the asciinema-player class.
document.addEventListener("DOMContentLoaded", (event) => {
    for (const player of document.getElementsByClassName("render-asciinema-player")) {
        const dimensions = {
            cols: player.dataset.cols,
            rows: player.dataset.rows,
        };
        AsciinemaPlayer.create(player.dataset.cast, player, dimensions);
    }
})