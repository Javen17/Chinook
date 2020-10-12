function deleteItem(url , itemId) {
    return fetch( url + itemId, {
        method: 'Delete'
    }).then(response => {
        if (response.status == 200) {
            alert('Eliminado');
            window.location.replace(url);
            return;
        }
    });
}

function modifyItem(url, itemId) {
    return fetch(url + itemId, {
        method: 'PUT'
    }).then(response => {
        if (response.status == 200) {
            alert('Modificado')
            window.location.replace(url);
            return;
        }
    });
}

function searchItem(url, searchString) {
    if (window.location.href.includes('/search-all/'))
        window.location.replace(url + '/search-all/' + 'filterValue=' + searchString);
}