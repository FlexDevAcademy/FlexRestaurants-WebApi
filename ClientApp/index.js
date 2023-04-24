const baseUrl = 'https://localhost:7080'

//1
async function getAll() {
    const response = await fetch(baseUrl)
    const json = await response.json()

    $('#getAllResponse').text(JSON.stringify(json))
}

//2
const itemIdInput = $('#itemIdInput')

const itemIdInputChange = () => {
    toggleButton(itemIdInput, $('#itemIdButton'))
}

async function getById() {
    await consumeData(`${baseUrl}/${itemIdInput.val()}`, $('#getByIdResponse'), $('#getByIdResponseCard'))
}

//3
const itemNameInput = $('#itemNameInput')

const itemNameInputChange = () => {
    toggleButton(itemNameInput, $('#itemNameButton'))
}

async function getByName() {
    await consumeData(`${baseUrl}/${itemNameInput.val()}`, $('#getByNameResponse'), $('#getByNameResponseCard'))
}

//4
const itemRatingInput = $('#itemRatingInput')

const itemRatingInputChange = () => {
    toggleButton(itemRatingInput, $('#itemRatingButton'))
}

async function getByRating() {
    await consumeData(`${baseUrl}/search?rating=${itemRatingInput.val()}`, $('#getByRatingResponse'), $('#getByRatingResponseCard'))
}

//other
async function consumeData(url, responsePlaceholder, responsePlaceholderCard) {
    const response = await fetch(url)
    const json = await response.json()

    $(responsePlaceholder).text(JSON.stringify(json))
    $(responsePlaceholderCard).empty()

    if (json.name && json.description && json.imageUrl && json.telephoneNumber) {
        $(responsePlaceholderCard).append(getTileMarkUp(json))
    } else {
        json.forEach(item => {
            if (item.name && item.description && item.imageUrl && item.telephoneNumber) {
                $(responsePlaceholderCard).append(getTileMarkUp(item))
            }
        })
    }
}

const getTileMarkUp = (json) => {
    return `<div class="d-flex m-2">
                <img class="mr-2" src="${json.imageUrl}" height="120" style="border-radius: 10px;"/>
                <div class="d-flex flex-column justify-content-between">
                    <h5>${json.name}</h5>
                    <h6 class="font-italic">"${json.description}"</h6>
                    <p class="mb-0">Ocena: ${json.rating}/5</p>
                    <p class="mb-0">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-telephone" viewBox="0 0 16 16">
                            <path d="M3.654 1.328a.678.678 0 0 0-1.015-.063L1.605 2.3c-.483.484-.661 1.169-.45 1.77a17.568 17.568 0 0 0 4.168 6.608 17.569 17.569 0 0 0 6.608 4.168c.601.211 1.286.033 1.77-.45l1.034-1.034a.678.678 0 0 0-.063-1.015l-2.307-1.794a.678.678 0 0 0-.58-.122l-2.19.547a1.745 1.745 0 0 1-1.657-.459L5.482 8.062a1.745 1.745 0 0 1-.46-1.657l.548-2.19a.678.678 0 0 0-.122-.58L3.654 1.328zM1.884.511a1.745 1.745 0 0 1 2.612.163L6.29 2.98c.329.423.445.974.315 1.494l-.547 2.19a.678.678 0 0 0 .178.643l2.457 2.457a.678.678 0 0 0 .644.178l2.189-.547a1.745 1.745 0 0 1 1.494.315l2.306 1.794c.829.645.905 1.87.163 2.611l-1.034 1.034c-.74.74-1.846 1.065-2.877.702a18.634 18.634 0 0 1-7.01-4.42 18.634 18.634 0 0 1-4.42-7.009c-.362-1.03-.037-2.137.703-2.877L1.885.511z"/>
                        </svg>
                        <a href="${json.telephoneNumber}">${json.telephoneNumber}</a>
                    </p>
                </div>
            </div>`
}

const toggleButton = (input, buttonElement) => {
    if (input.val()) {
        buttonElement.attr("disabled", false)
    } else {
        buttonElement.attr("disabled", true)
    }
}