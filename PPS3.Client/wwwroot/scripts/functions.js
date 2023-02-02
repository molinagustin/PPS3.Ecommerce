function showAlert(message) {
    alert(message);
}

function getUserId() {
    return document.getElementById('UsAct').value
}

function DescargarPDF(filename, byteBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + byteBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}