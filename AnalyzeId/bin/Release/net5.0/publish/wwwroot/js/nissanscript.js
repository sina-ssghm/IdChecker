var endpoint = 'https://services.idvpacific.com.au';
var callback = 'https://webhook.site/8049b229-ecf5-4212-8b85-e6b3607ec768';
var applicationId = String(localStorage.getItem('applicationId'));
var domain = window.location.origin;
//if (applicationId == 'null' && !window.location.href.endsWith('Front') && !window.location.href.endsWith('Index')) {
//    window.location.replace('/Home/Index')
//}
var ocrData = localStorage.getItem('ocrData') != null ? JSON.parse(localStorage.getItem('ocrData')) : null;
var apiUsername = 'hamid';
var apiPassword = '5150Murphy.mo71*';

var elementsAdded = localStorage.getItem('elementsAdded') != null ? localStorage.getItem('elementsAdded') == 'true' : false;

function navigate() {
    window.location.replace('/reg/test.html');
}
function spin1() {
    document.querySelector('.spin').classList.remove('d-none');
    document.querySelector('.spin').style.display = "block"
}
function unspin1() {
    document.querySelector('.spin').classList.add('d-none');
    document.querySelector('.spin').style.display = "none"
}
function startApp() {
    spin1()
    document.querySelector('.detail').classList.add('d-none');
    var data = new FormData();
    data.append('Application_Title', 'VI Replication');
    data.append('Date_Format', 'yyyy-mm-dd');
    data.append('Expiration', '10Y');
    data.append('Branch_ID', '1b2cd180-7a57-11ec-ad77-7be29f4b9c78');

    var config = {
        method: 'post',
        url: `${endpoint}/api/Forms/Application`,
        headers: {
            'API-Username': apiUsername,
            'API-Password': apiPassword,
        },
        data: data
    };
    debugger
    axios(config)
        .then(function (response) {
            if (response.data.Message.Error == false) {
                debugger;
                applicationId = response.data.Result.Application_ID;
                localStorage.setItem('applicationId', applicationId);
                unspin1()
                document.querySelector('.detail').classList.remove('d-none');
                //CreateFrontQR();
            }
            else {
                console.log('Error');
                document.getElementById('frontQR').innerHTML = `<p>Error ocured. Please Try again.</p>`;
                unspin1()
                document.querySelector('.detail').classList.remove('d-none');
            }
        })
        .catch(function (error) {
            debugger;
            console.log(error.message);
            unspin1()
            document.querySelector('.detail').classList.remove('d-none');
        });
}

function CreateFrontQR() {
    var callbackurl = domain + `/VIApi/CreateDetails`
    var data = new FormData();
    data.append('Application_ID', applicationId);
    data.append('Element_Key', 'DLF');
    data.append('Element_Title', 'Driving Licence - Front');
    data.append('ID_Document_Code', '2');
    data.append('OCR', 'True');
    data.append('OCR_Engine', '2');
    data.append('QR_Size', '300');
    data.append('Base64_Result', 'True');
    data.append('Return_Link_Only', 'True');
    data.append('Desktop_Access', 'True');
    data.append('Callback_URL', callbackurl);

    var config = {
        method: 'post',
        url: `${endpoint}/api/Forms/Link`,
        headers: {
            'API-Username': apiUsername,
            'API-Password': apiPassword,
        },
        data: data
    };

    axios(config)
        .then(function (response) {
            console.log(response.data)
            document.getElementById('frontQR').innerHTML = `<a target='_blank' href='${response.data.Result.QR.Link}' ><img element-key="DLF" link='${response.data.Result.QR.Link}' src='data:image/png;base64,${response.data.Result.QR.Base64_File}' style='width:200px' /></a>`;
            checkLink('DLF', function () {
                window.location.replace('/VIApi/Back');
            })
        })
        .catch(function (error) {
            console.log(error);
        });

}


function CreateBackQR() {
    var data = new FormData();
    var callbackurl = domain + `/VIApi/CreateDetails`
    data.append('Application_ID', applicationId);
    data.append('Element_Key', 'DLB');
    data.append('Element_Title', 'Driving Licence - Back');
    data.append('Related_Element_Key', 'DLB');
    data.append('ID_Document_Code', '3');
    data.append('OCR', 'True');
    data.append('OCR_Engine', '2');
    data.append('Validation', 'True');
    data.append('QR_Size', '300');
    data.append('Base64_Result', 'True');
    data.append('Return_Link_Only', 'True');
    data.append('Desktop_Access', 'True');
    data.append('Callback_URL', callbackurl);
    var config = {
        method: 'post',
        url: `${endpoint}/api/Forms/Link`,
        headers: {
            'API-Username': apiUsername,
            'API-Password': apiPassword,
        },
        data: data
    };
    axios(config)
        .then(function (response) {
            console.log(response.data)
            document.getElementById('backQR').innerHTML = `<a target='_blank' href='${response.data.Result.QR.Link}' ><img element-key="DLB" link='${response.data.Result.QR.Link}' src='data:image/png;base64,${response.data.Result.QR.Base64_File}'  style='width:200px' /></a>`;
            checkLink('DLB', function () {
                window.location.replace('/VIApi/ClientInfo');
            })
        })
        .catch(function (error) {
            console.log(error);
        });
}


function checkLink(key, callback) {
    var timer = setInterval(() => {
               //url: `/VIApi/IsExistRequest?id=${applicationId}&type=${key}`,
        var config = {
            method: 'get',
            url: `/VIApi/IsExistRequest?id=${applicationId}&type=${key}`,
            headers: {
                'API-Username': apiUsername,
                'API-Password': apiPassword
            },

        };


        //if (response.data.Message.Error == false) {
        //    if (response.data.Result.Elements.Element_No_1.QR_Link.Status_Code == 17) {
        //        clearInterval(timer);
        //        callback();
        //    }
        //}
        //else {

        //    console.error('error in api result')
        //}
        axios(config)
            .then(function (response) {
                //console.log(response.data)
                if (response.data) {
                    clearInterval(timer);
                    callback();
                }
                // else {
                   // console.log('not exist ')
     
                //}
             
            })
            .catch(function (error) {
                console.log(error);
            });
    }, 3000);


}
function UpdateApplication(phoneNumber, Email) {
    var data = new FormData();

    data.append('Application_ID', applicationId);
    data.append('Email_Address', Email);
    data.append('Phone_Number', phoneNumber);

    var config = {
        method: 'put',
        url: endpoint + '/api/Forms/Application',
        headers: {
            'API-Username': apiUsername,
            'API-Password': apiPassword
        },
        data: data
    };

    axios(config)
        .then(function (response) {
            if (response.data.Message.Error == false) {
                console.log(response.data)
                window.location.replace('/VIApi/ConfirmData');
            }
            else {
                console.log('Error');
                document.getElementById('submit-form-btn').classList.remove('disabled')
            }


        })
        .catch(function (error) {
            console.log(error);
        });

}



function SubmitClientInfo() {
    var emailElem = document.getElementById('Email');
    var pnElem = document.getElementById('PhoneNumber');
    if (emailElem.value != '' && pnElem.value != '') {
        document.getElementById('submit-form-btn').classList.add('disabled')
        UpdateApplication(pnElem.value, emailElem.value);
    }
    else {
        var validation = document.getElementsByClassName('validation')[0];
        validation.classList.remove('d-none');
        validation.innerHTML = `<p class='mb-0'>Please Enter Mandatory Fields.</p>`
    }
}



function CreateNewElements() {
    document.getElementById('submit-form-btn').classList.add('disabled')
    var fnElem = document.getElementById('FirstName');
    var snElem = document.getElementById('Surname');
    var birthElem = document.getElementById('Birth');
    var licenceNumberElem = document.getElementById('LicenceNumber');
    var licenceExpiryElem = document.getElementById('LicenceExpiry');
    var addressElem = document.getElementById('Address');
    if (fnElem.value == '' || snElem.value == '' || birthElem.value == '' || licenceNumberElem.value == '' || licenceExpiryElem.value == '' || addressElem.value == '') {
        var validation = document.getElementsByClassName('validation')[0];
        validation.classList.remove('d-none');
        validation.innerHTML = `<p class='mb-0'>Please Enter All Fields.</p>`;
        return;
    }
    ocrData = {
        FirstName: fnElem.value,
        Surname: snElem.value,
        Birth: birthElem.value,
        LicenceNumber: licenceNumberElem.value,
        LicenceExpiry: licenceExpiryElem.value,
        Address: addressElem.value
    };
    localStorage.setItem('ocrData', JSON.stringify(ocrData));
    var data = JSON.stringify({
        "Elements": [
            { "Key": "E1", "Title": "First_Name", "Value": fnElem.value },
            { "Key": "E2", "Title": "Last_Name", "Value": snElem.value },
            { "Key": "E3", "Title": "Full_Name", "Value": fnElem.value + ' ' + snElem.value },
            { "Key": "E4", "Title": "Driving_Licence_Number", "Value": licenceNumberElem.value },
            { "Key": "E5", "Title": "Expiry_Date", "Value": licenceExpiryElem.value },
            { "Key": "E6", "Title": "Birth_Date", "Value": birthElem.value },
            { "Key": "E7", "Title": "Address", "Value": addressElem.value },
            { "Key": "E8", "Title": "T&C", "Value": "True" }
        ]
    });

    var config = {
        method: elementsAdded ? 'put' : 'post',
        url: endpoint + '/api/Forms/Element',
        headers: {
            'API-Username': apiUsername,
            'API-Password': apiPassword,
            'Application_ID': applicationId,
            'Content-Type': 'application/json'
        },
        data: data
    };

    axios(config)
        .then(function (response) {

            if (response.data.Message.Error == false) {
                console.log(response.data)
                localStorage.setItem('elementsAdded', 'true');
                window.location.replace('/Home/Signature');
            }
            else {
                console.log('Error');
                document.getElementById('submit-form-btn').classList.remove('disabled')
            }

        })
        .catch(function (error) {
            console.log(error);
        });

}

function ExecuteApplication(useCallback = false) {
    var data = new FormData();
    data.append('Application_ID', applicationId);
    data.append('Force_ReExecute', 'True');
    if (useCallback == true) {
        data.append('Secure_Callback_URL', callback);
    }
    var config = {
        method: 'post',
        url: endpoint + '/api/Forms/Execute',
        headers: {
            'API-Username': apiUsername,
            'API-Password': apiPassword,
        },
        data: data
    };

    return axios(config)

}

function FinalExecute() {
    return new Promise((resolve, reject) => {
        ExecuteApplication(true).then(function (response) {
            if (response.data.Message.Error == false) {

                var config = {
                    method: 'get',
                    url: endpoint + '/api/Forms/Result?Application_ID=' + applicationId + '&Element_Key=DLF',
                    headers: {
                        'API-Username': apiUsername,
                        'API-Password': apiPassword,
                    },

                };

                resolve(axios(config));
            }
            else {
                reject('Error in promise')
            }

        }).catch(function (error) {
            reject(error)
        });


    });
}

function GetApplicationResult() {
    return new Promise((resolve, reject) => {
        ExecuteApplication().then(function (response) {
            if (response.data.Message.Error == false) {

                var config = {
                    method: 'get',
                    url: endpoint + '/api/Forms/Result?Application_ID=' + applicationId + '&Element_Key=DLF',
                    headers: {
                        'API-Username': apiUsername,
                        'API-Password': apiPassword,
                    },

                };

                resolve(axios(config));
            }
            else {
                reject('Error in promise')
            }

        }).catch(function (error) {
            reject(error)
        });


    });
}

function SubmitImgBack() {
    document.getElementById('submit-form-btn').classList.add('disabled')

    var isEmpty = signaturePad.isEmpty();
    if (isEmpty) {
        var validation = document.getElementsByClassName('validation')[0];
        validation.classList.remove('d-none');
        validation.innerHTML = `<p class='mb-0'>Please Sign the pad.</p>`;
    }
    else {
        var img = signaturePad.toDataURL().split(',')[1];
        var blob = b64toBlob(img);
        UploadFile(blob, 'DLB', 'Driving Licence - Back', '3').then(function (response) {
            if (response.data.Message.Error == false) {
                console.log(response.data);
                ExecuteApplication(true).then(function (response) {
                    console.log(response.data);
                    window.location.replace('/VIApi/ThankYou');

                });
            }
            else {
                console.log(response.data);
                document.getElementById('submit-form-btn').classList.remove('disabled')

            }
        });

    }
}
function SubmitImgFront() {
    //document.getElementById('submit-form-btn').classList.add('disabled')
    debugger
    spin1()
    document.querySelector('.cameraDiv ').classList.add('d-none');
    var img = document.getElementById('File').files[0];
    debugger
        //var blob = b64toBlob(img);
    UploadFile(img, 'DLF', 'Driving Licence - Front', '2').then(function (response) {
            if (response.data.Message.Error == false) {
                console.log(response.data);
                ExecuteApplication(true).then(function (response) {
                    console.log(response.data);
                    //window.location.replace('/VIApi/ThankYou');
                    unspin1()
                    document.querySelector('.cameraDiv').classList.remove('d-none');
                    localStorage.setItem('uploadImgFront', "true");
                });
            }
            else {
                console.log(response.data);
                //document.getElementById('submit-form-btn').classList.remove('disabled')
                localStorage.setItem('uploadImgFront', "false");
                unspin1()
                document.querySelector('.error-alert').classList.remove('d-none');
                $(".error-message").html(response.data.Message)
            }
        });
}



function SubmitSignature() {
    document.getElementById('submit-form-btn').classList.add('disabled')
    var isEmpty = signaturePad.isEmpty();
    if (isEmpty) {
        var validation = document.getElementsByClassName('validation')[0];
        validation.classList.remove('d-none');
        validation.innerHTML = `<p class='mb-0'>Please Sign the pad.</p>`;
    }
    else {
        var img = signaturePad.toDataURL().split(',')[1];
        var blob = b64toBlob(img);
        UploadFile(blob, 'SIGN','Signature','0').then(function (response) {
            if (response.data.Message.Error == false) {
                console.log(response.data);
                ExecuteApplication(true).then(function (response) {
                    console.log(response.data);
                    window.location.replace('/VIApi/ThankYou');
                });
            }
            else {
                console.log(response.data);
                document.getElementById('submit-form-btn').classList.remove('disabled')
            }
        });

    }
}

function UploadFile(file,element_key,element_title,id_code) {
    return new Promise((resolve) => {
        // var data = file;
        debugger
        var data = new FormData();
        data.append('File', new File([file], "img.png"));
        var config = {
            method: 'post',
            url: `${endpoint}/api/Forms/File`,
            headers: {
                'API-Username': apiUsername,
                'API-Password': apiPassword,
                'Application_ID': applicationId,
                'Element_Key': element_key,
                'Element_Title': element_title,
                'ID_Document_Code': id_code,
                'OCR': 'False',
                'Content-Type': 'image/png'
            },
            data: data
        };
        resolve(axios(config));
    })
}
function convertDate(date) {
    var parseddate = new Date(date);
    return `${String(parseddate.getDate()).padStart(2, '0')}/${String(parseddate.getMonth()).padStart(2, '0')}/${parseddate.getFullYear()}`
}

const b64toBlob = (b64Data, contentType = 'image/png', sliceSize = 512) => {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];
    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        const slice = byteCharacters.slice(offset, offset + sliceSize);
        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }
    const blob = new Blob(byteArrays, { type: contentType });
    return blob;
}