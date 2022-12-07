import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import PropTypes from 'prop-types';

import { alertEnum, AlertType } from './AlertServices';
import { Alert } from 'bootstrap';

const propTypes = {
    id: PropTypes.string,
    fade: PropTypes.bool
};

export default function AlertsComponents() {
    const history = useHistory();
    const [alerts, setAlerts] = useState([]);

    useEffect(alert => {
        const subscription = alertEnum.onAlert(id)
            .subscribe(alert => {
                if(!alert.message){
                    setAlerts(alerts => {
                        const filteredAlerts = alerts.filter(x => x.keepAfterRouteChange);
                        filteredAlerts.forEach(x => delete x.keepAfterRouteChange);
                        return filteredAlerts;
                    });
                }
                else{
                    setAlerts(alerts => ([...alerts, alert]));

                    if(alert.autoClose){
                        setTimeout(() => removeAlert(alert), 3000);
                    }
                }
            });

        const historyUnlisten = history.listen(() => {
            alertEnum.clear(id);
        });

        return () => {
            subscription.unsubscribe();
            historyUnlisten();
        };
    }, []);

    const removeAlert = (alert) => {
        if(fade){
            const alertWithFade = {...alert, fade: true};
            setAlerts(alerts => alerts.map(x => x === alert ? alertWithFade : x));


            setTimeout(() => {
                setAlerts(alerts => alerts.filter(x => x != alertWithFade));
            }, 250);
        }
        else{
            setAlerts(alerts => alerts.filter(x => x !== alert));
        }
    }

    const cssClass = (alert) => {
        if(!alert) 
            return;

        const className = ['alert', 'alert-dismissable'];

        const alertTypeClass = {
            [AlertType.Success]: 'alert alert-success',
            [AlertType.Error]: 'alert alert-danger',
            [AlertType.Info]: 'alert alert-info',
            [AlertType.Warning]: 'alert alert-warning'
        };

        classNames.push(alertTypeClass[alert.type]);

        if(alert.fade)
            className.push('fade');

        return className.join(' ');

    } 

    if(!alerts.length) 
        return null;

    return (
        <div className='container'>
            <div className='m-3'>
                { alerts.map((alert, index) => {
                    <div key={index} className={cssClass(alert)}>
                        <a className='close' onClick={() => removeAlert(alert)}>&times;</a>
                        <span dangerouslySetInnerHTML={{_html: alert.message}}></span>
                    </div>
                })}
            </div>
        </div>
    );
}

AlertsComponents.propTypes = propTypes;
AlertsComponents.defaultProps = defaultProps;
export { AlertsComponents };