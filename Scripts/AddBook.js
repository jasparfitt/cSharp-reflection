let bookNumStart = 1;

const newBook = () => {
    let bookDiv = document.getElementById(`book-${bookNumStart}`);
    let bookSelect = bookDiv.children[0];
    let newBookDiv = bookDiv.cloneNode(true);
    bookNumStart++;
    newBookDiv.id = `book-${bookNumStart}`;
    bookSelect.setAttribute("readonly", true);
    bookSelect.setAttribute("tabindex", -1);
    bookSelect.classList.add("disable-input");
    bookDiv.classList.add("disable-div");
    let inputHolder = document.getElementById("input-holder");
    inputHolder.appendChild(newBookDiv);
}


