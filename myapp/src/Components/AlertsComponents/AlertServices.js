import { Subject } from 'rxjs'
import { filter } from 'rxjs/operators'

const alertSubject = new Subject();
const defaultId = 'default-alert';

export const alertEnum = {
    onAlert,
    success,
    error,
    info,
    warn,
    alert,
    clear
};

export const AlertType = {
    Success: 'Success',
    Error: 'Error',
    Info: "Info",
    Warning: 'Warning'
};

function onAlert(id = defaultId){
    return alertSubject.asObservable().pipe(filter(x => x && x.id === id));
}

function success(message, options) {
    alert({ ...options, type: AlertType.Success, message });
}

function error(message, options) {
    alert({ ...options, type: AlertType.Error, message });
}

function info(message, options) {
    alert({ ...options, type: AlertType.Info, message });
}

function warn(message, options) {
    alert({ ...options, type: AlertType.Warning, message });
}

// core alert method
function alert(alert) {
    alert.id = alert.id || defaultId;
    alertSubject.next(alert);
}

// clear alerts
function clear(id = defaultId) {
    alertSubject.next({ id });
}

