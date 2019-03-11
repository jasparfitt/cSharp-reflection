let bookNumStart = 1;
const dropdown = document.getElementById(`book-active`).cloneNode(true);


const newBook = (e) => {
    e.preventDefault();

    // Disable old input
    let bookDiv = document.getElementById(`book-active`);
    bookDiv.id = "";
    for (let n = 0; n < bookDiv.children.length; n++) {
        let bookSelect = bookDiv.children[n];
        if (bookSelect.nodeName == "SELECT" || bookSelect.nodeName == "INPUT") {
            console.log("hi")
            bookSelect.setAttribute("readonly", true);
            bookSelect.setAttribute("tabindex", -1);
            bookSelect.classList.add("disable-input");
            bookDiv.classList.add("disable-div");
        }
    }

    // Add new dropdown input
    let newBookDiv = dropdown.cloneNode(true);
    let inputHolder = document.getElementById("input-holder");
    inputHolder.appendChild(newBookDiv);

    // Change to manual input depending on state of button
    let otherButton = document.getElementById("other-button");
    if (otherButton.getAttribute("value") == "Back") {
        changeInput(false);
    }
}

const manualBook = (e) => {
    e.preventDefault();
    // change input to manual
    auto = false;
    changeInput(auto);
    changeButton(auto);
}

const autoBook = (e) => {
    e.preventDefault();
    // change input to auto
    auto = true;
    changeInput(auto);
    changeButton(auto);
}

const changeInput = (auto) => {
    // remove old input
    let oldActiveHolder = document.getElementById(`book-active`);
    let inputHolder = document.getElementById(`input-holder`);
    inputHolder.removeChild(oldActiveHolder);
    let newActiveHolder;
    if (auto) {
        // add new dropdown
        newActiveHolder = dropdown.cloneNode(true);
    } else {
        // add new manual text inputs
        newActiveHolder = document.createElement("div");
        newActiveHolder.id = "book-active";
        newActiveHolder.classList.add("book-input");
        let otherTitleInput = document.createElement("input");
        otherTitleInput.setAttribute("type", "textbox");

        let otherAuthorInput = otherTitleInput.cloneNode(true);
        otherTitleInput.setAttribute("name", "NewBooks");
        otherTitleInput.setAttribute("placeholder", "Book Title");
        otherAuthorInput.setAttribute("name", "NewAuthors");
        otherAuthorInput.setAttribute("placeholder", "Author");
        newActiveHolder.appendChild(otherTitleInput);
        newActiveHolder.appendChild(otherAuthorInput);
    }
    inputHolder.appendChild(newActiveHolder);
}

const changeButton = (auto) => {
    let otherButton = document.getElementById("other-button");
    if (auto) {
        // change button to switch to manual entry
        otherButton.setAttribute("value", "Other Book");
        otherButton.setAttribute("onclick", "manualBook(event)");
    } else {
        // change button to switch to auto entry
        otherButton.setAttribute("value", "Back");
        otherButton.setAttribute("onclick", "autoBook(event)");
    }
}